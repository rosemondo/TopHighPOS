using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Xml.Linq;
using TopHighPos.Business;
using TopHighPos.Business.RolesRepository;
using TopHighPos.Business.UsersRepository;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;
using TopHighPos.IBusiness;
using TopHighPos.IBusiness.RolesInterface;
using TopHighPos.IBusiness.UsersInterface;

namespace TopHighPos.UI.Controllers
{
    public class UsersController : Controller
    {
        IUserRepository iuserRepo;
        IRoleRepository irolesRepo;

        public UsersController() : this(new UserRepository(), new RoleRepository())
        {
        }

        public UsersController(IUserRepository iuserRepo, IRoleRepository irolesRepo)
        {
            this.iuserRepo = iuserRepo;
            this.irolesRepo = irolesRepo;
        }

        [HandleError]
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        //add new user
        [HttpPost]
        public ActionResult AddNewUser(string email_address, string password, int rolesid, int? id)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                Users model = new Users()
                {
                    id = (int)id,
                    email_address = email_address,
                    password = password,
                    rolesid = rolesid
                };

                if (id > 0 && id != null)
                {
                    iuserRepo.UpdateUser(model);
                }
                else
                {
                    iuserRepo.AddNewUser(model);
                }
                return RedirectToAction("GetUsersDetails", "Users");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //delete user from database
        public ActionResult DeleteUser(Users model)
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                iuserRepo.DeleteUser(model);
                return RedirectToAction("GetUsersDetails", "Users");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        //edit user using json
        public JsonResult EditUser(int id)
        {
            List<UserViewModel> userlist = new List<UserViewModel>();
            userlist = iuserRepo.GetAllUsers();
            return Json(userlist.SingleOrDefault(x => x.id == id), JsonRequestBehavior.AllowGet);
        }

        //get user list
        [HttpGet]
        [OutputCache(CacheProfile = "GetAll")]
        public ActionResult GetUsersDetails()
        {
            if (!string.IsNullOrEmpty(Session["UserName"] as string))
            {
                List<UserViewModel> userlist = new List<UserViewModel>();
                userlist = iuserRepo.GetAllUsers();
                return View(userlist);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        //check for user validity then login
        [HttpPost]
        public ActionResult Login(Users model)
        {
            if (ModelState.IsValid)
            {
                Users users = iuserRepo.ValidUser(model);
                if (users.id != 0)
                {
                    Session["UserId"] = users.rolesid;
                    Session["ShopId"] = users.shopid;
                    Session["UserName"] = users.email_address;
                    return RedirectToAction("Dashboard", "Home");
                }
                return View(model);
            }
            else
                return View(model);
        }

        //get user role
        public ActionResult GetRoles()
        {
            List<UsersRole> rolelist = new List<UsersRole>();
            rolelist = irolesRepo.GetAllRoles();

            return Json(rolelist.Select(x => new
            {
                RoleID = x.RolesId,
                RoleName = x.Roles
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //log user out from using the app
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}