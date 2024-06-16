using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;

namespace TopHighPos.IBusiness.UsersInterface
{
    public interface IUserRepository
    {
        void AddNewUser(Users model);
        void DeleteUser(Users model);
        List<UserViewModel> GetAllUsers();
        void UpdateUser(Users model);
        Users ValidUser(Users model);
    }
}
