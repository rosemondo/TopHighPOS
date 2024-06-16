using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;

namespace TopHighPos.IBusiness
{
    public interface IProductRepository
    {
        List<ProductViewModel> GetAllProduct();
        void AddNewProduct(ProductViewModel model);
        void UpdateProduct(ProductViewModel model);
        void DeleteProduct(int id);
    }
}
