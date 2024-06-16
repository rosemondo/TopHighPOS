using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain;

namespace TopHighPos.IBusiness.CategoriesInterface
{
    public interface ICategoryRepository
    {
        List<Categories> GetAllCategories();
        void AddCategory(Categories model);
        void UpdateCategory(Categories model);
        void DeleteCategory(int id);
    }
}
