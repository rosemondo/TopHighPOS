using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.IBusiness;

namespace TopHighPos.Business
{
    public class CustomerRepository : ICustomerRepository
    {
        private POSDBEntities db;

        public CustomerRepository()
        {
            db = new POSDBEntities();
        }

        //add new customer
        public void AddCustomers(Customers model)
        {
            try
            {
                Customer customer = new Customer()
                {
                    customername = model.customername,
                    custaddress = model.custaddress,
                    city = model.city,
                    state = model.state,
                    mobile = model.mobile,
                    email = model.email,
                    website = model.website,
                    createddate = model.createddate,
                    lastupdateddate = model.lastupdateddate,
                };
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       //delete customer from db table
        public void DeleteCustomers(int id)
        {
            try
            {
                var customer = db.Customers.FirstOrDefault(x => x.custid == id);
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get all customer
        public List<Customers> GetAllCustomers()
        {
            try
            {
                List<Customers> custlist = new List<Customers>();
                var list = db.Customers.ToList();
                foreach(var c in list)
                {
                    Customers cust = new Customers();
                    cust.custid = c.custid;
                    cust.customername = c.customername;
                    cust.custaddress = c.custaddress;
                    cust.city = c.city;
                    cust.state = c.state;
                    cust.mobile = c.mobile;
                    cust.email = c.email;
                    cust.website = c.website;
                    custlist.Add(cust);
                }
                return custlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //update customer
        public void UpdateCustomers(Customers model)
        {
            try
            {
                Customer cust = db.Customers.SingleOrDefault(x=> x.custid ==  model.custid);

                cust.customername = model.customername;
                cust.custaddress = model.custaddress;
                cust.city = model.city;
                cust.state = model.state;
                cust.mobile = model.mobile;
                cust.email= model.email;
                cust.website = model.website;
                cust.lastupdateddate = model.lastupdateddate;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
