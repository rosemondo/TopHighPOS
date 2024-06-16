using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;
using TopHighPos.IBusiness.UsersInterface;

namespace TopHighPos.Business.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        private POSDBEntities db;

        public UserRepository()
        {
            db = new POSDBEntities();
        }

        //add new users
        public void AddNewUser(Users model)
        {
            try
            {
                var f_password = PasswordHasher.GetMD5(model.password);

                User userobj = new User()
                {
                    email_address = model.email_address,
                    password = f_password,
                    rolesid = model.rolesid,
                    createddate = model.createddate,
                    lastupdateddate = model.lastupdateddate,
                };
                db.Users.Add(userobj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error can not Insert New User" +  ex.Message.ToString());
            }
        }

        //delete user by id
        public void DeleteUser(Users model)
        {
            try
            {
                User userdata = db.Users.Where(x => x.id == model.id).FirstOrDefault<User>();
                db.Users.Remove(userdata);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error occured. Can not delete users" + ex.Message.ToString());
            }
        }

        //get all user list
        public List<UserViewModel> GetAllUsers()
        {
            try
            {
                List<UserViewModel> list = new List<UserViewModel>();

                var userlist = (from users in db.Users
                                join
                                role in db.UserRoles on
                                users.rolesid equals role.RolesId
                                select new
                                {
                                    id = users.id,
                                    email_address = users.email_address,
                                    password = users.password,
                                    shopid = users.shopid,
                                    roleid = users.rolesid,
                                    roles = role.Roles
                                }).ToList();

                foreach (var u in userlist)
                {
                    UserViewModel dto = new UserViewModel();

                    dto.id = u.id;
                    dto.email_address = u.email_address;
                    dto.password = u.password;
                    dto.rolesid = u.roleid;
                    dto.shopid = Convert.ToInt32(u.shopid);
                    dto.roles = u.roles;
                    list.Add(dto);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred when populating user list" + ex.Message.ToString());
            }
        }

        //update user by id
        public void UpdateUser(Users model)
        {
            try
            {
                User users = db.Users.SingleOrDefault(x => x.id == model.id);

                users.id = model.id;
                users.email_address = model.email_address;
                users.password = PasswordHasher.GetMD5(model.password);
                users.rolesid = model.rolesid;
                users.lastupdateddate = model.lastupdateddate;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //check to see if user is valid
        public Users ValidUser(Users model)
        {
            try
            {
                Users dto = new Users();

                var f_password = PasswordHasher.GetMD5(model.password);

                User user = db.Users.SingleOrDefault(x => x.email_address == model.email_address && x.password == f_password);
                if (user != null && user.id != 0)
                {
                    dto.id = user.id;
                    dto.email_address = user.email_address;
                    dto.rolesid = user.rolesid;
                    dto.shopid = user.shopid;
                }

                return dto;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred when trying to login: " + ex.Message.ToString());
            }
        }
    }
}
