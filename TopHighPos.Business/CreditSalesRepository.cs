using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;
using TopHighPos.IBusiness;

namespace TopHighPos.Business
{
    public class CreditSalesRepository : ICreditSalesRepository
    {
        private POSDBEntities db;

        public CreditSalesRepository()
        {
            db = new POSDBEntities();
        }

        //Print invoice by order number
        public InvoiceViewModel GenerateInvoice(Guid odernumber)
        {
            InvoiceViewModel order = new InvoiceViewModel();
            List<InvoiceOrderDetailsViewModel> orderlist = new List<InvoiceOrderDetailsViewModel>();

            try
            {
                var orders = (from O in db.CreditSalesOders
                              join C in db.Customers on O.custid equals C.custid
                              join S in db.Shops on O.shopid equals S.shopid
                              where O.odernumber == odernumber
                              select O).FirstOrDefault();

                order.oderid = orders.oderid;
                order.odernumber = orders.odernumber;
                order.orderdate = Convert.ToDateTime(orders.orderdate);
                order.pay_meth = orders.pay_meth;
                order.subtotal = Convert.ToDouble(orders.subtotal);
                order.vat = Convert.ToDouble(orders.vat);
                order.nettotal = Convert.ToDouble(orders.nettotal);
                order.po_num = Convert.ToInt32(orders.po_num);
                order.due_date = Convert.ToDateTime(orders.due_date);
                order.customername = orders.Customer.customername;
                order.custaddress = orders.Customer.custaddress + " " + " - " + " " + orders.Customer.city + "," + orders.Customer.state;
                order.mobile = orders.Customer.mobile;
                order.email = orders.Customer.email;
                order.salesagent = orders.salesagent;
                order.shopn_name = orders.Shop.shopn_name;
                order.address = orders.Shop.shop_address + " " + " - " + " " + orders.Shop.shop_city;
                order.contact = orders.Shop.shop_phone;
                order.location = orders.Shop.shop_location;

                var details = (from d in db.CreditSalesOrderDetails where d.odernumber == order.odernumber select d).ToList();

                foreach (var d in details)
                {
                    InvoiceOrderDetailsViewModel obj = new InvoiceOrderDetailsViewModel();
                    obj.product = d.product;
                    obj.qty = Convert.ToDouble(d.qty);
                    obj.salesprice = Convert.ToDouble(d.salesprice);
                    obj.totals = Convert.ToDouble(d.totals);
                    orderlist.Add(obj);
                }

                order.InvoiceDetails = orderlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return order;
        }

        //Get daialy invoice by shop
        public InvoiceViewModel GetDailyCreditSales(DateTime DayDate, int shopid)
        {
            InvoiceViewModel sales = new InvoiceViewModel();
            List<InvoiceOrderDetailsViewModel> creditsaleslist = new List<InvoiceOrderDetailsViewModel>();

            try
            {
                var saleslist = (from s in db.CreditSalesOders.Where(x => x.orderdate == DayDate && x.shopid == shopid)
                                 join
                                 d in db.CreditSalesOrderDetails on
                                 s.odernumber equals d.odernumber
                                 group d by new { d.product, d.salesprice } into grp
                                 select new
                                 {
                                     Product = grp.Key.product,
                                     Qty = grp.Sum(d => d.qty),
                                     Price = grp.Key.salesprice,
                                     Totals = grp.Key.salesprice * grp.Sum(d => d.qty),
                                 }).ToList();

                foreach (var sl in saleslist)
                {
                    InvoiceOrderDetailsViewModel obj = new InvoiceOrderDetailsViewModel();
                    obj.product = sl.Product;
                    obj.qty = Convert.ToDouble(sl.Qty);
                    obj.salesprice = Convert.ToDouble(sl.Price);
                    obj.totals = Convert.ToDouble(sl.Totals);
                    creditsaleslist.Add(obj);
                }

                sales.InvoiceDetails = creditsaleslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sales;
        }

        //Search invoice by date
        public InvoiceViewModel GetDailyCreditSalesByDate(DateTime datefrom, DateTime dateto, int shopid)
        {
            InvoiceViewModel sales = new InvoiceViewModel();
            List<InvoiceOrderDetailsViewModel> creditsaleslist = new List<InvoiceOrderDetailsViewModel>();
            try
            {
                var saleslist = (from s in db.CreditSalesOders.Where(x => x.orderdate >= datefrom.Date && x.orderdate <= dateto.Date && x.shopid == shopid)
                                 join
                                 d in db.CreditSalesOrderDetails on
                                 s.odernumber equals d.odernumber
                                 group d by new { d.product, d.salesprice } into grp
                                 select new
                                 {
                                     Product = grp.Key.product,
                                     Qty = grp.Sum(d => d.qty),
                                     Price = grp.Key.salesprice,
                                     Totals = grp.Key.salesprice * grp.Sum(d => d.qty),
                                 }).ToList();

                foreach (var sl in saleslist)
                {
                    InvoiceOrderDetailsViewModel obj = new InvoiceOrderDetailsViewModel();
                    obj.product = sl.Product;
                    obj.qty = Convert.ToDouble(sl.Qty);
                    obj.salesprice = Convert.ToDouble(sl.Price);
                    obj.totals = Convert.ToDouble(sl.Totals);
                    creditsaleslist.Add(obj);
                }

                sales.InvoiceDetails = creditsaleslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sales;
        }

        //Get invoice list
        public List<InvoiceListViewModel> GetInvoiceList(DateTime date, int shopid)
        {
            List<InvoiceListViewModel> invoicetLists = new List<InvoiceListViewModel>();
            try
            {
                var receiptlist = (from r in db.CreditSalesOders.Where(x => x.orderdate == date && x.shopid == shopid)
                                   join c in db.Customers
                                   on r.custid equals c.custid
                                   select r
                                   ).ToList();

                foreach (var l in receiptlist)
                {
                    InvoiceListViewModel obj = new InvoiceListViewModel();
                    obj.ID = l.oderid;
                    obj.odernumber = l.odernumber;
                    obj.Date = Convert.ToDateTime(l.orderdate);
                    obj.Customer = l.Customer.customername;
                    invoicetLists.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return invoicetLists;
        }

        //Get invoice list by date
        public List<InvoiceListViewModel> GetInvoiceListByDate(DateTime datefrom, DateTime dateto, int shopid)
        {
            List<InvoiceListViewModel> invoiceLists = new List<InvoiceListViewModel>();
            try
            {
                var receiptlist = (from r in db.CreditSalesOders.Where(x => x.orderdate >= datefrom.Date && x.orderdate <= dateto.Date && x.shopid == shopid)
                                   join c in db.Customers
                                   on r.custid equals c.custid
                                   select r
                                   ).ToList();

                foreach (var l in receiptlist)
                {
                    InvoiceListViewModel obj = new InvoiceListViewModel();
                    obj.ID = l.oderid;
                    obj.odernumber = l.odernumber;
                    obj.Date = Convert.ToDateTime(l.orderdate);
                    obj.Customer = l.Customer.customername;
                    invoiceLists.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return invoiceLists;
        }

        //insert sales order
        public void InsertCreditSalelsOrder(CreditSalesOders model)
        {
            try
            {
                CreditSalesOder bskdata = new CreditSalesOder()
                {
                    orderdate = model.orderdate,
                    odernumber = model.odernumber,
                    pay_meth = model.pay_meth,
                    subtotal = model.subtotal,
                    vat = model.vat,
                    nettotal = model.nettotal,
                    po_num = model.po_num,
                    due_date = model.due_date,
                    custid = model.custid,
                    salesagent = model.salesagent,
                    shopid = model.shopid,
                    createddate = model.createddate,
                    lastupdatedate = model.lastupdatedate,
                };
                db.CreditSalesOders.Add(bskdata);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Insert order details
        public void InsertCreditSalelsOrderDetails(CreditSalesOrderDetails orderdetails)
        {
            try
            {
                CreditSalesOrderDetail bskdetails = new CreditSalesOrderDetail()
                {
                    odernumber = orderdetails.odernumber,
                    product = orderdetails.product,
                    qty = orderdetails.qty,
                    salesprice = orderdetails.salesprice,
                    unitcost = orderdetails.unitcost,
                    totals = orderdetails.totals,
                    createddate = orderdetails.createddate,
                    lastupdateddate = orderdetails.lastupdateddate,
                };
                db.CreditSalesOrderDetails.Add(bskdetails);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
