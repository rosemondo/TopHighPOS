using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;
using TopHighPos.IBusiness;

namespace TopHighPos.Business.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private POSDBEntities db;

        public ProductRepository()
        {
            db = new POSDBEntities();
        }

        //add new product
        public void AddNewProduct(ProductViewModel model)
        {
            try
            {
                Product prod = new Product()
                {
                    catid = model.catid,
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    Sku = model.Sku,
                    Qty = model.Qty,
                    UnitCost = model.UnitCost,
                    SalesPrice = model.SalesPrice,
                    ReoderPoint = model.ReoderPoint,
                    shopid = model.shopid,
                    supid = model.supid,
                    manufacturingdate = model.manufacturingdate,
                    expirydate = model.expirydate,
                    createddate = model.createddate,
                    lastupdateddate = model.lastupdateddate,
                };
                db.Products.Add(prod);
                db.SaveChanges();

                int latestproId = prod.proid;

                //Insert Product history
                Producthistory prohist = new Producthistory()
                {
                    proid = latestproId,
                    qty_in = model.Qty,
                    qty_out = 0,
                    shopid = model.shopid,
                    createddate = model.createddate,
                };
                db.Producthistories.Add(prohist);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error. Can not add new product" + ex.Message.ToString());
            }
        }

        //delete product from db table
        public void DeleteProduct(int id)
        {
            try
            {
                var prod = db.Products.FirstOrDefault(x => x.proid == id);
                db.Products.Remove(prod);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get all product
        public List<ProductViewModel> GetAllProduct()
        {
            List<ProductViewModel> productsList = new List<ProductViewModel>();

            var prodlist = (from prod in db.Products
                            join
                            cat in db.Categories on
                            prod.catid equals cat.catid
                            select new
                            {
                                ID = prod.proid,
                                ProductName = prod.ProductName,
                                ProductDescription = prod.ProductDescription,
                                UnitCost = prod.UnitCost,
                                SalesPrice = prod.SalesPrice,
                                Qty = prod.Qty,
                                Barcode = prod.Sku,
                                ReoderPoint = prod.ReoderPoint,
                                Category = cat.cate,
                                shopid = prod.shopid,
                                catid = prod.catid,
                                supid = prod.supid,
                                manufacturingdate = prod.manufacturingdate,
                                expirydate = prod.expirydate,
                            }).ToList();

            foreach (var prod in prodlist)
            {
                ProductViewModel dto = new ProductViewModel();
                dto.proid = prod.ID;
                dto.ProductName = prod.ProductName;
                dto.ProductDescription = prod.ProductDescription;
                dto.UnitCost = Convert.ToDouble(prod.UnitCost);
                dto.SalesPrice = Convert.ToDouble(prod.SalesPrice);
                dto.Qty = Convert.ToDouble(prod.Qty);
                dto.Sku = prod.Barcode;
                dto.ReoderPoint = Convert.ToDouble(prod.ReoderPoint);
                dto.categories = prod.Category;
                dto.shopid = prod.shopid;
                dto.catid = prod.catid;
                dto.supid = prod.supid;
                dto.manufacturingdate = prod.manufacturingdate;
                dto.expirydate = prod.expirydate;
                productsList.Add(dto);
            }

            return productsList;
        }

        //update product
        public void UpdateProduct(ProductViewModel model)
        {
            try
            {
                Product prod = db.Products.SingleOrDefault(x => x.proid == model.proid);

                prod.proid  = model.proid;
                prod.catid = model.catid;
                prod.ProductName = model.ProductName;
                prod.ProductDescription = model.ProductDescription;
                prod.Sku = model.Sku;
                prod.Qty = model.Qty;
                prod.UnitCost = model.UnitCost;
                prod.SalesPrice = model.SalesPrice;
                prod.ReoderPoint = model.ReoderPoint;
                prod.supid = model.supid;
                prod.shopid = model.shopid;
                prod.manufacturingdate = model.manufacturingdate;
                prod.expirydate = model.expirydate;
                prod .lastupdateddate = model.lastupdateddate;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
