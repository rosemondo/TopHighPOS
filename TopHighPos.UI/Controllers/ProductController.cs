using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TopHighPos.Business.CategoryRepository;
using TopHighPos.Business.ChartOfAccountRepository;
using TopHighPos.Business.JVRepository;
using TopHighPos.Business.ProductRepository;
using TopHighPos.Business.ShopRepository;
using TopHighPos.Business.SuppliersRepository;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;
using TopHighPos.IBusiness;
using TopHighPos.IBusiness.CategoriesInterface;
using TopHighPos.IBusiness.ChartOfAccountInterface;
using TopHighPos.IBusiness.JournalVoucherInterface;
using TopHighPos.IBusiness.ShopInterface;
using TopHighPos.IBusiness.SuppliersInterface;

namespace TopHighPos.UI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository iProductRepo;
        private ICategoryRepository iCateRepo;
        private ISuppliersRepository iSupRepo;
        private IShopRepository iShopRepo;
        private IChartOfAccountRepository iChartRepo;
        private IJournalVoucherRepository iJVRepo;
        public ProductController() : this(new ProductRepository(), new CategoryRepository(), new SupplierRepository(), new ShopRepository(), new ChartOfAccountRepository(), new JournalVoucherRepository())
        {
        }
        public ProductController(IProductRepository iProductRepo, ICategoryRepository iCateRepo, ISuppliersRepository iSupRepo, IShopRepository iShopRepo, IChartOfAccountRepository iChartRepo, IJournalVoucherRepository iJVRepo)
        {
            this.iProductRepo = iProductRepo;
            this.iCateRepo = iCateRepo;
            this.iSupRepo = iSupRepo;
            this.iShopRepo = iShopRepo;
            this.iChartRepo = iChartRepo;
            this.iJVRepo = iJVRepo;
        }

        [HandleError]
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        //Adding new product to database
        [HttpPost]
        public ActionResult AddNewProduct(int catid, string ProductName, string ProductDescription, string Sku, double Qty, double UnitCost, double SalesPrice, double ReoderPoint, int supid, int shopid, DateTime manufacturingdate, DateTime expirydate, int coaid, int coaid2, int? proid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                string Name = (string)Session["UserName"];
                int rolesid = (int)Session["UserId"];

                //pass data to product model
                ProductViewModel model = new ProductViewModel()
                {
                    proid = (int)proid,
                    catid = catid,
                    ProductName = ProductName,
                    ProductDescription = ProductDescription,
                    Sku = Sku,
                    Qty = Qty,
                    UnitCost = UnitCost,
                    SalesPrice = SalesPrice,
                    ReoderPoint = ReoderPoint,
                    supid = supid,
                    shopid = shopid,
                    manufacturingdate = manufacturingdate,
                    expirydate = expirydate,
                };

                //pass data to journal voucher table to capture accounting entries (debit entires)
                JournalVoucherModel jv = new JournalVoucherModel()
                {
                    cash_code = Guid.NewGuid().ToString(),
                    jv_date = DateTime.Now.Date,
                    coaid = coaid,
                    debit = Qty * UnitCost,
                    credit = 0,
                    descriptions = "Purchase of " + Qty + "" + " of " + ProductName + ' ' + "@" + ' ' + (UnitCost * Qty) + "",
                    ent_time = DateTime.Now.TimeOfDay,
                    shopid = shopid,
                    RolesId = rolesid,
                };

                //pass data to journal voucher table to capture accounting entries (credit entries)
                JournalVoucherModel jv2 = new JournalVoucherModel()
                {
                    cash_code = jv.cash_code,
                    jv_date = DateTime.Now.Date,
                    coaid = coaid2,
                    debit = 0,
                    credit = Qty * UnitCost,
                    descriptions = "Purchase of " + Qty + "" + " of " + ProductName + ' ' + "@" + ' ' + (UnitCost * Qty) + "",
                    ent_time = DateTime.Now.TimeOfDay,
                    shopid = shopid,
                    RolesId = rolesid,
                };

                if (proid > 0 && proid != null)
                {
                    iProductRepo.UpdateProduct(model);
                    ModelState .Clear ();
                }
                else
                {
                    iProductRepo.AddNewProduct(model);
                    iJVRepo.AddnewJournal1(jv);
                    iJVRepo.AddnewJournal2(jv2);
                    ModelState.Clear();
                }
                return RedirectToAction("ProductList", "Product");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //Get product by id
        [HttpGet]
        public JsonResult EditProduct(int proid)
        {
            List<ProductViewModel> prolist = new List<ProductViewModel>();
            prolist = iProductRepo.GetAllProduct();
            return Json(prolist.SingleOrDefault(m => m.proid == proid), JsonRequestBehavior.AllowGet);

        }

        //Delete product data
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                iProductRepo.DeleteProduct(id);
                return RedirectToAction("ProductList", "Product");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //pass data to journal voucher table to capture accounting entries
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetCateList()
        {
            List<Categories> catlist = new List<Categories>();
            catlist = iCateRepo.GetAllCategories();

            return Json(catlist.Select(x => new
            {
                ID = x.catid,
                Cate = x.cate
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //Populate suppliers control
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetSuppList()
        {
            List<Suppliers> supliers = new List<Suppliers>();
            supliers = iSupRepo.GetAllSuppliers();

            return Json(supliers.Select(x => new
            {
                ID = x.supid,
                Supplier = x.suppliername
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //Populate shop control
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetShopList()
        {
            List<Shops> shoplist = new List<Shops>();
            shoplist = iShopRepo.GetAllShop();
            return Json(shoplist.Select(x => new
            {
                ID = x.shopid,
                Shop = x.shop_location
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //Populate A/c control1
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetAccInventList()
        {
            List<ChartOfAccounts> acclist = new List<ChartOfAccounts>();
            acclist = iChartRepo.GetAllAccounts();
            return Json(acclist.Select(x => new
            {
                ID = x.coaid,
                Account = x.coa_name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //Populate A/c control2
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetAccCashtList()
        {
            List<ChartOfAccounts> acclist = new List<ChartOfAccounts>();
            acclist = iChartRepo.GetAllAccounts();
            return Json(acclist.Select(x => new
            {
                ID = x.coaid,
                Account = x.coa_name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //get and cache all product list
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult ProductList()
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                try
                {
                    List<ProductViewModel> prolist = new List<ProductViewModel>();
                    prolist = iProductRepo.GetAllProduct();
                    return View(prolist);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred when populating product list" + ex.Message);
                }
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}