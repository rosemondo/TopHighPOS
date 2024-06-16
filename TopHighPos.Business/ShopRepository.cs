using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.IBusiness.ShopInterface;

namespace TopHighPos.Business.ShopRepository
{
    public class ShopRepository : IShopRepository
    {
        private POSDBEntities db;

        public ShopRepository()
        {
            db = new POSDBEntities();
        }

        //get all shop list
        public List<Shops> GetAllShop()
        {
            try
            {
                List<Shops> shops = new List<Shops>();
                var list = db.Shops.ToList();
                foreach (var s in list)
                {
                    Shops shp = new Shops();
                    shp.shopid = s.shopid;
                    shp.shopn_name = s.shopn_name;
                    shp.shop_address = s.shop_address;
                    shp.shop_city = s.shop_city;
                    shp.shop_state = s.shop_state;
                    shp.shop_phone = s.shop_phone;
                    shp.shop_email = s.shop_email;
                    shp.shop_website = s.shop_website;
                    shp.shop_location = s.shop_location;
                    shops.Add(shp);
                }
                return shops;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //add new shop
        public void AddShop(Shops model)
        {
            try
            {
                Shop shop = new Shop()
                {
                    shopn_name = model.shopn_name,
                    shop_address = model.shop_address,
                    shop_city = model.shop_city,
                    shop_state = model.shop_state,
                    shop_phone = model.shop_phone,
                    shop_email = model.shop_email,
                    shop_website = model.shop_website,
                    shop_location = model.shop_location,
                    createddate = model.createddate,
                    lastupdateddate = model.lastupdateddate,
                };
                db.Shops.Add(shop);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //update shop by id
        public void UpdateShop(Shops model)
        {
            try
            {
                Shop shop = db.Shops.SingleOrDefault(x => x.shopid == model.shopid);

                shop.shopn_name = model.shopn_name;
                shop.shop_address = model.shop_address;
                shop.shop_city = model.shop_city;
                shop.shop_state = model.shop_state;
                shop.shop_phone = model.shop_phone;
                shop.shop_email = model.shop_email;
                shop.shop_website = model.shop_website;
                shop.shop_location = model.shop_location;
                shop.lastupdateddate = model.lastupdateddate;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //delete shop by id
        public void DeleteShop(int shopid)
        {
            try
            {
                var shop = db.Shops.FirstOrDefault(x => x.shopid == shopid);
                db.Shops.Remove(shop);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
