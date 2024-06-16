using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopHighPos.Business;
using TopHighPos.Business.SuppliersRepository;
using TopHighPos.Domain;
using TopHighPos.IBusiness;
using TopHighPos.IBusiness.SuppliersInterface;

namespace TopHighPos.UI.Controllers
{
    public class SuppliersController : Controller
    {

        private ISuppliersRepository iSupRepo;

        public SuppliersController() : this(new SupplierRepository())
        {

        }

        public SuppliersController(ISuppliersRepository iSupRepo)
        {
            this.iSupRepo = iSupRepo;
        }

        [HandleError]
        // GET: Suppliers
        public ActionResult Index()
        {
            return View();
        }

        //get supplier data from database
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult SuppliersList()
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                List<Suppliers> suplist = new List<Suppliers>();
                suplist = iSupRepo.GetAllSuppliers();
                return View(suplist);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //add new supplier to db
        [HttpPost]
        public ActionResult AddNewSupplier(string suppliername, string supaddress, string city, string state, string mobile, string email, string website, int? supid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                Suppliers model = new Suppliers()
                {
                    supid = (int)supid,
                    suppliername = suppliername,
                    supaddress = supaddress,
                    city = city,
                    state = state,
                    mobile = mobile,
                    email = email,
                    website = website,
                };

                if (supid > 0 && supid != null)
                {
                    iSupRepo.UpdateSuppliers(model);
                }
                else
                {
                    iSupRepo.AddNewSuppliers(model);
                }
                return RedirectToAction("suppliersList", "Suppliers");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //get customer by id
        [HttpGet]
        public JsonResult GetSupplierById(int supid)
        {
            List<Suppliers> suplist = new List<Suppliers>();
            suplist = iSupRepo.GetAllSuppliers();
            return Json(suplist.SingleOrDefault(x => x.supid == supid), JsonRequestBehavior.AllowGet);
        }

        //delete customer by id
        public ActionResult DeleteSupplier(int id)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                iSupRepo.DeleteSuppliers(id);
                return RedirectToAction("suppliersList", "Suppliers");
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }
    }
}