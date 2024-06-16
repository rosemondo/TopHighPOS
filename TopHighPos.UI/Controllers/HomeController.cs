using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TopHighPos.UI.Controllers
{
    public class HomeController : Controller
    {
        [HandleError]
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }

    }
}