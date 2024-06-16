using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain;

namespace TopHighPos.IBusiness
{
    public interface ICustomerRepository
    {
        List<Customers> GetAllCustomers();
        void AddCustomers(Customers model);
        void UpdateCustomers(Customers model);
        void DeleteCustomers(int id);
    }
}
