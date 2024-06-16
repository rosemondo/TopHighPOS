using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.IBusiness.ChartOfAccountInterface;

namespace TopHighPos.Business.ChartOfAccountRepository
{
    public class ChartOfAccountRepository : IChartOfAccountRepository
    {
        private POSDBEntities db;

        public ChartOfAccountRepository()
        {
            db = new POSDBEntities();
        }

        //get all chart of account list
        public List<ChartOfAccounts> GetAllAccounts()
        {
            try
            {
                List<ChartOfAccounts> acclist = new List<ChartOfAccounts>();
                var list = db.ChartOfAccounts.ToList();
                foreach (var a in list)
                {
                    ChartOfAccounts chart = new ChartOfAccounts();
                    chart.coaid = a.coaid;
                    chart.coa_code = a.coa_code;
                    chart.coa_name = a.coa_name;
                    chart.coa_group = a.coa_group;
                    chart.coa_sub_group = a.coa_sub_group;
                    chart.coa_cate = a.coa_cate;
                    chart.coa_cf = a.coa_cf;
                    chart.coa_ocbfy = a.coa_ocbfy;
                    chart.coa_cogm = a.coa_cogm;
                    acclist.Add(chart);
                }
                return acclist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
