using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain.ViewModel;
using TopHighPos.Domain;

namespace TopHighPos.IBusiness.SuppliersInterface
{
    public interface ISuppliersRepository
    {
        List<Suppliers> GetAllSuppliers();
        void AddNewSuppliers(Suppliers model);
        void UpdateSuppliers(Suppliers model);
        void DeleteSuppliers(int id);
    }
}
