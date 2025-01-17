﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TopHighPos.UI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View("Error");
        }

        public ActionResult InternalServer()
        {
            Response.StatusCode = 500;
            return View("Error");
        }
    }
}