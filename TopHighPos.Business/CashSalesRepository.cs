using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;
using TopHighPos.IBusiness;
using static TopHighPos.Domain.ViewModel.POSReceiptViewModel;

namespace TopHighPos.Business
{
    public class CashSalesRepository : ICashSalesRepository
    {
        private POSDBEntities db;

        public CashSalesRepository()
        {
            db = new POSDBEntities();
        }

        //insert sales order
        public void InsertCashSalelsOrder(CashSalesOders model)
        {
            try
            {
                CashSalesOder bskdata = new CashSalesOder()
                {
                    orderdate = model.orderdate,
                    odernumber = model.odernumber,
                    pay_meth = model.pay_meth,
                    subtotal = model.subtotal,
                    vat = model.vat,
                    sale_disc = model.sale_disc,
                    nettotal = model.nettotal,
                    amt_rece = model.amt_rece,
                    amt_change = model.amt_change,
                    custid = model.custid,
                    salesagent = model.salesagent,
                    shopid = model.shopid,
                    createddate = model.createddate,
                    lastupdatedate = model.lastupdatedate,
                };
                db.CashSalesOders.Add(bskdata);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Insert order details
        public void InsertCashSalelsOrderDetails(CashSalesOrderDetails orderdetails)
        {
            try
            {
                CashSalesOrderDetail bskdetails = new CashSalesOrderDetail()
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
                db.CashSalesOrderDetails.Add(bskdetails);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Print receipt by order number
        public POSReceiptViewModel GenerateReceipt(Guid odernumber)
        {
            POSReceiptViewModel order = new POSReceiptViewModel();
            List<ReceiptOrderDetailsViewModel> orderlist = new List<ReceiptOrderDetailsViewModel>();

            try
            {
                var orders = (from O in db.CashSalesOders
                              join C in db.Customers on O.custid equals C.custid
                              join S in db.Shops on O.shopid equals S.shopid
                              where O.odernumber == odernumber
                              select O).FirstOrDefault();

                order.orderid = orders.oderid;
                order.odernumber = orders.odernumber;
                order.orderdate = Convert.ToDateTime(orders.orderdate);
                order.pay_meth = orders.pay_meth;
                order.subtotal = Convert.ToDouble(orders.subtotal);
                order.vat = Convert.ToDouble(orders.vat);
                order.sale_disc = Convert.ToDouble(orders.sale_disc);
                order.nettotal = Convert.ToDouble(orders.nettotal);
                order.amt_rece = Convert.ToDouble(orders.amt_rece);
                order.amt_change = Convert.ToDouble(orders.amt_change);
                order.CustomerName = orders.Customer.customername;
                order.salesagent = orders.salesagent;
                order.shopn_name = orders.Shop.shopn_name;
                order.address = orders.Shop.shop_address + " " + " - " + " " + orders.Shop.shop_city;
                order.contact = orders.Shop.shop_phone;
                order.location = orders.Shop.shop_location;

                var details = (from d in db.CashSalesOrderDetails where d.odernumber == order.odernumber select d).ToList();

                foreach (var d in details)
                {
                    ReceiptOrderDetailsViewModel obj = new ReceiptOrderDetailsViewModel();
                    obj.product = d.product;
                    obj.qty = Convert.ToDouble(d.qty);
                    obj.salesprice = Convert.ToDouble(d.salesprice);
                    obj.totals = Convert.ToDouble(d.totals);
                    orderlist.Add(obj);
                }

                order.ReceiptDetails = orderlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return order;
        }

        //Get daialy sales by shop
        public POSReceiptViewModel GetDailyCashSales(DateTime DayDate, int shopid)
        {
            POSReceiptViewModel sales = new POSReceiptViewModel();
            List<ReceiptOrderDetailsViewModel> cashsaleslist = new List<ReceiptOrderDetailsViewModel>();

            try
            {
                var saleslist = (from s in db.CashSalesOders.Where(x => x.orderdate == DayDate && x.shopid == shopid)    
                                 join
                                 d in db.CashSalesOrderDetails on
                                 s.odernumber equals d.odernumber
                                 group d by new {d.product, d.salesprice } into grp
                                 select new
                                 {
                                     Product = grp.Key.product,
                                     Qty = grp.Sum(d => d.qty),
                                     Price = grp.Key.salesprice,
                                     Totals = grp.Key.salesprice * grp.Sum(d => d.qty),
                                 }).ToList();

                foreach (var sl in saleslist)
                {
                    ReceiptOrderDetailsViewModel obj = new ReceiptOrderDetailsViewModel();
                    obj.product = sl.Product;
                    obj.qty = Convert.ToDouble(sl.Qty);
                    obj.salesprice = Convert.ToDouble(sl.Price);
                    obj.totals = Convert.ToDouble(sl.Totals);
                    cashsaleslist.Add(obj);
                }

                sales.ReceiptDetails = cashsaleslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sales;
        }

        //Search cash sales by date
        public POSReceiptViewModel GetDailyCashSalesByDate(DateTime datefrom, DateTime dateto, int shopid)
        {
            POSReceiptViewModel sales = new POSReceiptViewModel();
            List<ReceiptOrderDetailsViewModel> cashsaleslist = new List<ReceiptOrderDetailsViewModel>();
            try
            {
                var saleslist = (from s in db.CashSalesOders.Where(x => x.orderdate >= datefrom.Date && x.orderdate <= dateto.Date && x.shopid == shopid)
                                 join
                                 d in db.CashSalesOrderDetails on
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
                    ReceiptOrderDetailsViewModel obj = new ReceiptOrderDetailsViewModel();
                    obj.product = sl.Product;
                    obj.qty = Convert.ToDouble(sl.Qty);
                    obj.salesprice = Convert.ToDouble(sl.Price);
                    obj.totals = Convert.ToDouble(sl.Totals);
                    cashsaleslist.Add(obj);
                }

                sales.ReceiptDetails = cashsaleslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sales;
        }

        //Get sales receipt list
        public List<ReceiptListViewModel> GetReceiptList(DateTime date, int shopid)
        {
            List<ReceiptListViewModel> receiptLists = new List<ReceiptListViewModel>();
            try
            {
                var receiptlist = (from r in db.CashSalesOders.Where(x => x.orderdate == date  && x.shopid == shopid)
                                   join c in db.Customers
                                   on r.custid equals c.custid select r
                                   ).ToList();

                foreach (var l in receiptlist)
                {
                    ReceiptListViewModel obj = new ReceiptListViewModel();
                    obj.ID = l.oderid;
                    obj.odernumber = l.odernumber;
                    obj.Date = Convert.ToDateTime(l.orderdate);
                    obj.Customer = l.Customer.customername;
                    receiptLists.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receiptLists;

        }
        //Get sales receipt list by date
        public List<ReceiptListViewModel> GetReceiptListByDate(DateTime datefrom, DateTime dateto, int shopid)
        {
            List<ReceiptListViewModel> receiptLists = new List<ReceiptListViewModel>();
            try
            {
                var receiptlist = (from r in db.CashSalesOders.Where(x => x.orderdate >= datefrom.Date && x.orderdate <= dateto.Date && x.shopid == shopid)
                                   join c in db.Customers
                                   on r.custid equals c.custid
                                   select r
                                   ).ToList();

                foreach (var l in receiptlist)
                {
                    ReceiptListViewModel obj = new ReceiptListViewModel();
                    obj.ID = l.oderid;
                    obj.odernumber = l.odernumber;
                    obj.Date = Convert.ToDateTime(l.orderdate);
                    obj.Customer = l.Customer.customername;
                    receiptLists.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receiptLists;
        }
    }
}
