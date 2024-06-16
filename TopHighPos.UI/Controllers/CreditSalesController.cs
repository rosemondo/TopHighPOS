using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopHighPos.Business.ChartOfAccountRepository;
using TopHighPos.Business.ProductRepository;
using TopHighPos.Business.ShopRepository;
using TopHighPos.Business;
using TopHighPos.IBusiness.ChartOfAccountInterface;
using TopHighPos.IBusiness.ShopInterface;
using TopHighPos.IBusiness;
using TopHighPos.Domain.ViewModel;
using TopHighPos.Domain;

namespace TopHighPos.UI.Controllers
{
    public class CreditSalesController : Controller
    {
        private IProductRepository iProductRepo;
        private IChartOfAccountRepository iChartRepo;
        private ICustomerRepository iCustRepo;
        private ICreditSalesRepository iCreditRepo;
        private IShopRepository iShopRepo;

        public CreditSalesController() : this(new ProductRepository(), new ChartOfAccountRepository(), new CustomerRepository(), new CreditSalesRepository(), new ShopRepository())
        {

        }

        public CreditSalesController(IProductRepository iProductRepo, IChartOfAccountRepository iChartRepo, ICustomerRepository iCustRepo, ICreditSalesRepository iCreditRepo, IShopRepository iShopRepo)
        {
            this.iProductRepo = iProductRepo;
            this.iChartRepo = iChartRepo;
            this.iCustRepo = iCustRepo;
            this.iCreditRepo = iCreditRepo;
            this.iShopRepo = iShopRepo;
        }

        [HandleError]
        // GET: CreditSales
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

        //credit sales forms
        public ActionResult CreditSales()
        {
            List<ProductViewModel> prolist = new List<ProductViewModel>();
            prolist = iProductRepo.GetAllProduct();
            ViewBag.Product = (from p in prolist
                               select new SelectListItem()
                               {
                                   Text = p.ProductName,
                                   Value = p.proid.ToString()
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
            return RedirectToAction("CreditSales");
        }

        //remove from cart
        public ActionResult RemoveFromBasket(int proid)
        {
            List<Basket> basket = (List<Basket>)Session["cart"];
            int index = IsInCart(proid);
            basket.RemoveAt(index);
            Session["cart"] = basket;
            return RedirectToAction("CreditSales");
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
            return RedirectToAction("CreditSales");
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

        //Save credit sales order to database
        public ActionResult SaveChasOrder(CreditSalesOders order)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                string Name = (string)Session["UserName"];
                int shopid = (int)Session["ShopId"];
                //Insert
                CreditSalesOders model = new CreditSalesOders()
                {
                    orderdate = order.orderdate,
                    odernumber = order.odernumber,
                    pay_meth = order.pay_meth,
                    subtotal = order.subtotal,
                    vat = order.vat,
                    nettotal = order.nettotal,
                    po_num = order.po_num,
                    due_date = order.due_date,
                    custid = order.custid,
                    salesagent = Name,
                    shopid = shopid,
                };
                iCreditRepo.InsertCreditSalelsOrder(model);

                List<Basket> basket = (List<Basket>)Session["cart"];

                //Insert order details
                foreach (var item in basket)
                {
                    CreditSalesOrderDetails orderdetails = new CreditSalesOrderDetails();
                    orderdetails.odernumber = order.odernumber;
                    orderdetails.product = item.product;
                    orderdetails.qty = item.qty;
                    orderdetails.salesprice = item.price;
                    orderdetails.unitcost = item.cost;
                    orderdetails.totals = item.qty * item.price;
                    iCreditRepo.InsertCreditSalelsOrderDetails(orderdetails);
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
        public ActionResult PrintInvoice(Guid odernumber)
        {
            var order = iCreditRepo.GenerateInvoice(odernumber);
            return View(order);
        }

        //Print receipt by order number
        [HttpGet]
        [OutputCache(CacheProfile = "SingleParameter")]
        public ActionResult PreviewInvoice(Guid odernumber)
        {
            var order = iCreditRepo.GenerateInvoice(odernumber);
            return View(order);
        }


        //Get daialy credit sales by shop
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult CreditSalesReport()
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

                var sales = iCreditRepo.GetDailyCreditSales(DayDate, shopid);
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
        public ActionResult SearchCreditSales(DateTime datefrom, DateTime dateto, int shopid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                var sales = iCreditRepo.GetDailyCreditSalesByDate(datefrom, dateto, shopid);
                return PartialView("CreditSalesReportPartial", sales);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }


        //Get sales receipt
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetInvoiceList()
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

                var list = iCreditRepo.GetInvoiceList(date, shopid);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //Get sales receipt
        [HttpPost]
        [OutputCache(CacheProfile = "MultipleParameters")]
        public ActionResult GeInvoiceListByDate(DateTime datefrom, DateTime dateto, int shopid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {

                var list = iCreditRepo.GetInvoiceListByDate(datefrom, dateto, shopid);
                return PartialView("InvoiceListPartial", list);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}