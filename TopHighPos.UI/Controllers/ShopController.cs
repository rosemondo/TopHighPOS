using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopHighPos.Business.ShopRepository;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.IBusiness.ShopInterface;

namespace TopHighPos.UI.Controllers
{
    public class ShopController : Controller
    {
        private IShopRepository iShopRepo;

        public ShopController() : this(new ShopRepository())
        {
        }

        public ShopController(IShopRepository iShopRepo)
        {
            this.iShopRepo = iShopRepo;
        }
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        //get shop list
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetShopList()
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                List<Shops> shoplist = new List<Shops>();
                shoplist = iShopRepo.GetAllShop();
                return View(shoplist);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }


        //add new shop to database
        [HttpPost]
        public ActionResult AddNewShop(string shopn_name, string shop_address,string shop_city, string shop_state, string shop_phone, string shop_email, string shop_website, string shop_location, int? shopid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                Shops model = new Shops()
                {
                    shopid = (int)shopid,
                    shopn_name = shopn_name,
                    shop_address = shop_address,
                    shop_city = shop_city,
                    shop_state = shop_state,
                    shop_phone = shop_phone,
                    shop_email = shop_email,
                    shop_website = shop_website,
                    shop_location = shop_location,
                };

                if (shopid > 0 && shopid != null)
                {
                    iShopRepo.UpdateShop(model);
                }
                else
                {
                    iShopRepo.AddShop(model);
                }
                return RedirectToAction("GetShopList", "Shop");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }


        //fetch shop list from table
        [HttpGet]
        public JsonResult GetShopById(int shopid)
        {
            List<Shops> shoplist = new List<Shops>();
            shoplist = iShopRepo.GetAllShop();
            return Json(shoplist.SingleOrDefault(m => m.shopid == shopid), JsonRequestBehavior.AllowGet);
        }


        //delete shop from table
        [HttpPost]
        public ActionResult DeleteShop(int shopid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                iShopRepo.DeleteShop(shopid);
                return RedirectToAction("GetShopList", "Shop");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}