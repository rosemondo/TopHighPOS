using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.IBusiness.CategoriesInterface;

namespace TopHighPos.Business.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private POSDBEntities db;

        public CategoryRepository()
        {
            db = new POSDBEntities();
        }

        //add new category
        public void AddCategory(Categories model)
        {
            try
            {
                Category cates = new Category()
                {
                    cate = model.cate,
                    createddate = model.createddate,
                    lastupdateddate = model.lastupdateddate,
                };
                db.Categories.Add(cates);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //delete categeory from table
        public void DeleteCategory(int id)
        {
            try
            {
                var cat = db.Categories.FirstOrDefault(x => x.catid == id);
                db.Categories.Remove(cat);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get all categories
        public List<Categories> GetAllCategories()
        {
            try
            {
                List<Categories> catlist = new List<Categories>();
                var list = db.Categories.ToList();
                foreach (var c in list)
                {
                    Categories cat = new Categories();
                    cat.catid = c.catid;
                    cat.cate = c.cate;
                    catlist.Add(cat);
                }
                return catlist;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Category list error" + ex.Message.ToString());
            }
        }

        //update category
        public void UpdateCategory(Categories model)
        {
            try
            {
                Category cat = db.Categories.SingleOrDefault(x => x.catid == model.catid);

                cat.cate = model.cate;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
