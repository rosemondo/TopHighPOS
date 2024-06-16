using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopHighPos.Business;
using TopHighPos.Domain;
using TopHighPos.IBusiness;

namespace TopHighPos.UI.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository iCustRepo;

        public CustomerController() : this(new CustomerRepository()) 
        {
        
        }

        public CustomerController (ICustomerRepository iCustRepo)
        {
            this.iCustRepo = iCustRepo;
        }

        [HandleError]
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        //get customer data from database
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult CustomerList()
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                List<Customers> custlist = new List<Customers>();
                custlist = iCustRepo.GetAllCustomers();
                return View(custlist);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //add new customer to db
        [HttpPost]
        public ActionResult AddNewCustomer(string customername, string custaddress, string city, string state,string mobile ,string email, string website,int? custid)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                Customers model = new Customers()
                {
                    custid = (int)custid,
                    customername = customername,
                    custaddress = custaddress,
                    city = city,
                    state = state,
                    mobile = mobile,
                    email = email,
                    website = website,
                };

                if (custid > 0 && custid != null)
                {
                    iCustRepo.UpdateCustomers(model);
                }
                else
                {
                    iCustRepo.AddCustomers(model);
                }
                return RedirectToAction("CustomerList", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //get customer by id
        [HttpGet]
        public JsonResult GetCustomerById(int custid)
        {
            List<Customers> custlist = new List<Customers>();
            custlist = iCustRepo.GetAllCustomers();
            return Json(custlist.SingleOrDefault(x=> x.custid== custid),JsonRequestBehavior.AllowGet);
        }

        //delete customer by id
        public ActionResult DeleteCustomer(int id)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                iCustRepo.DeleteCustomers(id);
                return RedirectToAction("CustomerList", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}