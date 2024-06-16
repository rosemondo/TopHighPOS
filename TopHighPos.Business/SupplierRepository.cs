using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.IBusiness.SuppliersInterface;

namespace TopHighPos.Business.SuppliersRepository
{
    public class SupplierRepository : ISuppliersRepository
    {
        private POSDBEntities db;

        public SupplierRepository()
        {
            db = new POSDBEntities();
        }

        //add new supplier
        public void AddNewSuppliers(Suppliers model)
        {
            try
            {
                Supplier supplier = new Supplier()
                {
                    suppliername = model.suppliername,
                    supaddress = model.supaddress,
                    city = model.city,
                    state = model.state,
                    mobile = model.mobile,
                    email = model.email,
                    website = model.website,
                    createddate = model.createddate,
                    lastupdateddate = model.lastupdateddate,
                };
                db.Suppliers.Add(supplier);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //delete supplier by id
        public void DeleteSuppliers(int id)
        {
            try
            {
                var supplier = db.Suppliers.FirstOrDefault(x => x.supid == id);
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get all suppleirs 
        public List<Suppliers> GetAllSuppliers()
        {
            try
            {
                List<Suppliers> suplist = new List<Suppliers>();
                var list = db.Suppliers.ToList();
                foreach ( var s in list )
                {
                    Suppliers sups = new Suppliers();
                    sups.supid = s.supid;
                    sups.suppliername = s.suppliername;
                    sups.supaddress = s.supaddress;
                    sups.city = s.city;
                    sups.state = s.state;
                    sups.mobile = s.mobile;
                    sups.email = s.email;
                    suplist.Add(sups);   
                };
                return suplist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //update suppliers by is
        public void UpdateSuppliers(Suppliers model)
        {
            try
            {
                Supplier sup = db.Suppliers.SingleOrDefault(x => x.supid == model.supid);

                sup.suppliername = model.suppliername;
                sup.supaddress = model.supaddress;
                sup.city = model.city;
                sup.state = model.state;
                sup.mobile = model.mobile;
                sup.email = model.email;
                sup.website = model.website;
                sup.lastupdateddate = model.lastupdateddate;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
