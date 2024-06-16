using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopHighPos.Business.CategoryRepository;
using TopHighPos.Domain;
using TopHighPos.IBusiness.CategoriesInterface;

namespace TopHighPos.UI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository iCateRepo;

        public CategoryController() : this(new  CategoryRepository()) 
        { 
        }

        public CategoryController (ICategoryRepository iCateRepo)
        {
            this.iCateRepo = iCateRepo;
        }

        [HandleError]
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        //get category list
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetCategoryList()
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                List<Categories> catlist = new List<Categories>();
                catlist = iCateRepo.GetAllCategories();
                return View(catlist);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //add new category to database
        [HttpPost]
        public ActionResult AddNewCategory(string cate, int? catid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                Categories model = new Categories()
                {
                    catid = (int)catid,
                    cate = cate,
                };

                if (catid > 0 && catid != null)
                {
                    iCateRepo.UpdateCategory(model);
                }
                else
                {
                    iCateRepo.AddCategory(model);
                }
                return RedirectToAction("GetCategoryList", "Category");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //fetch category list from table
        [HttpGet]
        public JsonResult GetCategoryById(int catid)
        {
            List<Categories> catelist = new List<Categories>();
            catelist = iCateRepo.GetAllCategories();
            return Json(catelist.SingleOrDefault(m=> m.catid == catid), JsonRequestBehavior.AllowGet);
        }

        //delete category from table
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                iCateRepo.DeleteCategory(id);
                return RedirectToAction("GetCategoryList", "Category");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}