using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain;

namespace TopHighPos.IBusiness.ShopInterface
{
    public interface IShopRepository
    {
        List<Shops> GetAllShop();
        void AddShop(Shops model);
        void UpdateShop(Shops model);
        void DeleteShop(int shopid);
    }
}
