using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using TopHighPos.Business;
using TopHighPos.Business.ChartOfAccountRepository;
using TopHighPos.Business.ProductRepository;
using TopHighPos.Business.ShopRepository;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;
using TopHighPos.IBusiness;
using TopHighPos.IBusiness.ChartOfAccountInterface;
using TopHighPos.IBusiness.ShopInterface;

namespace TopHighPos.UI.Controllers
{
    public class CashSalesController : Controller
    {
        private IProductRepository iProductRepo;
        private IChartOfAccountRepository iChartRepo;
        private ICustomerRepository iCustRepo;
        private ICashSalesRepository iCashRepo;
        private IShopRepository iShopRepo;

        public CashSalesController() : this(new ProductRepository(), new ChartOfAccountRepository(), new CustomerRepository(), new CashSalesRepository(), new ShopRepository())
        {

        }

        public CashSalesController(IProductRepository iProductRepo, IChartOfAccountRepository iChartRepo, ICustomerRepository iCustRepo, ICashSalesRepository iCashRepo, IShopRepository iShopRepo)
        {
            this.iProductRepo = iProductRepo;
            this.iChartRepo = iChartRepo;
            this.iCustRepo = iCustRepo;
            this.iCashRepo = iCashRepo;
            this.iShopRepo = iShopRepo;
        }

        [HandleError]
        // GET: CashSales
        public ActionResult Index()
        {
            return View();
        }

        //fecth product by id
        public JsonResult GetProductDataByID(int id)
        {
            List<ProductViewModel> prolist = new List<ProductViewModel>();
            prolist = iProductRepo.GetAllProduct();
            return Json(prolist.Where(x => x.proid == id), JsonRequestBehavior.AllowGet);
        }

        //cash sales forms
        public ActionResult CashSales()
        {
            List<ProductViewModel> prolist = new List<ProductViewModel>();
            prolist = iProductRepo.GetAllProduct();
            ViewBag.Product = (from p in prolist
                               select new SelectListItem()
                               {
                                   Text = p.ProductName,
                                   Value = p.proid.ToString()
                               }).ToList();

            List<ChartOfAccounts> chartlist = new List<ChartOfAccounts>();
            chartlist = iChartRepo.GetAllAccounts();
            ViewBag.ChartOfAccount = (from ch in chartlist.Where(x => x.coa_group == "CURRENT ASSETS")
                                      select new SelectListItem()
                                      {
                                          Text = ch.coa_name,
                                          Value = ch.coaid.ToString()
                                      }).ToList();

            List<Customers> custlist = new List<Customers>();
            custlist = iCustRepo.GetAllCustomers();
            ViewBag.Customers = (from c in custlist
                                 select new SelectListItem()
                                 {
                                     Text = c.customername,
                                     Value = c.custid.ToString()
                                 }).ToList();

            ViewBag.otp = Guid.NewGuid();

            return View();
        }

        //Add to cashsales basket
        public ActionResult AddToBasket(string txtproduct, double txtqty, double price, double cost, int proid, Guid basketid)
        {
            if (Session["cart"] == null)
            {
                List<Basket> basket = new List<Basket>();
                basket.Add(new Basket() { product = txtproduct, qty = txtqty, price = price, cost = cost, basketid = basketid, proid = proid });
                Session["cart"] = basket;
            }
            else
            {
                List<Basket> basket = (List<Basket>)Session["cart"];
                int index = IsInCart(proid);
                if (index != -1)
                {
                    basket[index].qty++;
                }
                else
                {
                    basket.Add(new Basket() { product = txtproduct, qty = txtqty, price = price, cost = cost, basketid = basketid, proid = proid });
                }
                Session["cart"] = basket;
            }
            return RedirectToAction("CashSales");
        }

        //remove from cart
        public ActionResult RemoveFromBasket(int proid)
        {
            List<Basket> basket = (List<Basket>)Session["cart"];
            int index = IsInCart(proid);
            basket.RemoveAt(index);
            Session["cart"] = basket;
            return RedirectToAction("CashSales");
        }

        //update cart
        public ActionResult UpdateCart(FormCollection fmc)
        {
            string[] qty = fmc.GetValues("qty");
            List<Basket> basket = (List<Basket>)Session["cart"];
            for (int i = 0; i < basket.Count; i++)
            {
                basket[i].qty = Convert.ToInt32(qty[i]);
            }
            Session["cart"] = basket;
            return RedirectToAction("CashSales");
        }

        //check if product is cart then update
        public int IsInCart(int proid)
        {
            List<Basket> basket = (List<Basket>)Session["cart"];
            for (int i = 0; i < basket.Count; i++)
            {
                if (basket[i].proid == proid)
                {
                    return i;
                }
            }
            return -1;
        }

        //Save cash sales order to database
        public ActionResult SaveChasOrder(CashSalesOders order)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                string Name = (string)Session["UserName"];
                int shopid = (int)Session["ShopId"];
                //Insert
                CashSalesOders model = new CashSalesOders()
                {
                    odernumber = order.odernumber,
                    pay_meth = order.pay_meth,
                    subtotal = order.subtotal,
                    vat = order.vat,
                    sale_disc = order.sale_disc,
                    nettotal = order.nettotal,
                    amt_rece = order.amt_rece,
                    amt_change = order.amt_change,
                    custid = order.custid,
                    salesagent = Name,
                    shopid = shopid,
                };
                iCashRepo.InsertCashSalelsOrder(model);

                List<Basket> basket = (List<Basket>)Session["cart"];

                //Insert order details
                foreach (var item in basket)
                {
                    CashSalesOrderDetails orderdetails = new CashSalesOrderDetails();
                    orderdetails.odernumber = order.odernumber;
                    orderdetails.product = item.product;
                    orderdetails.qty = item.qty;
                    orderdetails.salesprice = item.price;
                    orderdetails.unitcost = item.cost;
                    orderdetails.totals = item.qty * item.price;
                    iCashRepo.InsertCashSalelsOrderDetails(orderdetails);
                }
                Session.Remove("cart");
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //Print receipt by order number
        [HttpGet]
        [OutputCache(CacheProfile = "SingleParameter")]
        public ActionResult PrintReceipt(Guid odernumber)
        {
            var order = iCashRepo.GenerateReceipt(odernumber);
            return View(order);
        }

        //Print receipt by order number
        [HttpGet]
        [OutputCache(CacheProfile = "SingleParameter")]
        public ActionResult PreviewReceipt(Guid odernumber)
        {
            var order = iCashRepo.GenerateReceipt(odernumber);
            return View(order);
        }


        //Get daialy sales by shop
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult CashSalesReport()
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                List<Shops> shoplist = new List<Shops>();
                shoplist = iShopRepo.GetAllShop();
                ViewBag.Shop = (from p in shoplist
                                select new SelectListItem()
                                {
                                    Text = p.shopn_name,
                                    Value = p.shopn_name
                                }).ToList();

                int shopid = (int)Session["ShopId"];
                DateTime DayDate = DateTime.Now.Date;

                var sales = iCashRepo.GetDailyCashSales(DayDate, shopid);
                return View(sales);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //get shop id
        [HttpGet]
        [OutputCache(CacheProfile = "SingleParameter")]
        public JsonResult GetShopID(string name)
        {
            List<Shops> shoplist = new List<Shops>();
            shoplist = iShopRepo.GetAllShop();
            return Json(shoplist.Where(x => x.shopn_name == name), JsonRequestBehavior.AllowGet);
        }

        //Search cash sales by date
        [HttpPost]
        [OutputCache(CacheProfile = "MultipleParameters")]
        public ActionResult SearchCashSales(DateTime datefrom, DateTime dateto, int shopid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                var sales = iCashRepo.GetDailyCashSalesByDate(datefrom, dateto, shopid);
                return PartialView("CashSalesReportPartial", sales);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //Get sales receipt list
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetReceiptList()
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                List<Shops> shoplist = new List<Shops>();
                shoplist = iShopRepo.GetAllShop();
                ViewBag.Shop = (from p in shoplist
                                select new SelectListItem()
                                {
                                    Text = p.shopn_name,
                                    Value = p.shopn_name
                                }).ToList();

                int shopid = (int)Session["ShopId"];
                DateTime date = DateTime.Now.Date;

                var list = iCashRepo.GetReceiptList(date, shopid);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //Get sales receipt list by date
        [HttpPost]
        [OutputCache(CacheProfile = "MultipleParameters")]
        public ActionResult GetReceiptListByDate(DateTime datefrom, DateTime dateto, int shopid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {

                var list = iCashRepo.GetReceiptListByDate(datefrom, dateto, shopid);
                return PartialView("SalesReceiptPartial", list);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}