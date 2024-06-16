using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.IBusiness.RolesInterface;

namespace TopHighPos.Business.RolesRepository
{
    public class RoleRepository : IRoleRepository
    {
        private POSDBEntities db;

        public RoleRepository()
        {
            db = new POSDBEntities();
        }

        //get all user roles
        public List<UsersRole> GetAllRoles()
        {
            try
            {
                List<UsersRole> rolelist = new List<UsersRole>();

                var list = db.UserRoles.ToList();

                foreach (var r in list)
                {
                    UsersRole dto = new UsersRole();
                    dto.RolesId = r.RolesId;
                    dto.Roles = r.Roles;
                    rolelist.Add(dto);
                }
                return rolelist;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred when trying to login: " + ex.Message);
            }
        }
    }
}
