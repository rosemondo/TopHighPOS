using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain.ViewModel;
using TopHighPos.Domain;

namespace TopHighPos.IBusiness
{
    public interface ICreditSalesRepository
    {
        void InsertCreditSalelsOrder(CreditSalesOders model);
        void InsertCreditSalelsOrderDetails(CreditSalesOrderDetails orderdetails);
        InvoiceViewModel GenerateInvoice(Guid odernumber);
        InvoiceViewModel GetDailyCreditSales(DateTime DayDate, int shopid);
        InvoiceViewModel GetDailyCreditSalesByDate(DateTime datefrom, DateTime dateto, int shopid);
        List<InvoiceListViewModel> GetInvoiceList(DateTime date, int shopid);
        List<InvoiceListViewModel> GetInvoiceListByDate(DateTime datefrom, DateTime dateto, int shopid);
    }
}
