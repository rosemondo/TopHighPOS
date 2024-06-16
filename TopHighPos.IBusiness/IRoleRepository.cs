using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain;

namespace TopHighPos.IBusiness.RolesInterface
{
    public interface IRoleRepository
    {
        List<UsersRole> GetAllRoles();
    }
}
