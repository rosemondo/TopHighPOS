using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain;
using TopHighPos.Domain.ViewModel;
using static TopHighPos.Domain.ViewModel.POSReceiptViewModel;

namespace TopHighPos.IBusiness
{
    public interface ICashSalesRepository
    {
        void InsertCashSalelsOrder(CashSalesOders model);
        void InsertCashSalelsOrderDetails(CashSalesOrderDetails orderdetails);
        POSReceiptViewModel GenerateReceipt(Guid odernumber);
        POSReceiptViewModel GetDailyCashSales(DateTime DayDate, int shopid);
        POSReceiptViewModel GetDailyCashSalesByDate(DateTime datefrom, DateTime dateto, int shopid);
        List<ReceiptListViewModel> GetReceiptList(DateTime date, int shopid);
        List<ReceiptListViewModel> GetReceiptListByDate(DateTime datefrom, DateTime dateto, int shopid);
    }
}
