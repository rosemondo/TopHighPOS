using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Data;
using TopHighPos.Domain;
using TopHighPos.IBusiness.JournalVoucherInterface;

namespace TopHighPos.Business.JVRepository
{
    public class JournalVoucherRepository : IJournalVoucherRepository
    {
        private POSDBEntities db;

        public JournalVoucherRepository()
        {
            db = new POSDBEntities();
        }

        //add new journal entries debit
        public void AddnewJournal1(JournalVoucherModel jv)
        {
            try
            {
                JournalVoucher journal = new JournalVoucher()
                {
                    cash_code = jv.cash_code,
                    jv_date = jv.jv_date,
                    coaid = jv.coaid,
                    debit = jv.debit,
                    credit = jv.credit,
                    descriptions = jv.descriptions,
                    ent_time = jv.ent_time,
                    shopid = jv.shopid,
                    RolesId = jv.RolesId,
                    createddate = jv.createddate,
                    lastupdateddate = jv.lastupdateddate,
                };
                db.JournalVouchers.Add(journal);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex ;
            }
        }

        //add new journal entries credit
        public void AddnewJournal2(JournalVoucherModel jv2)
        {
            try
            {
                JournalVoucher journal = new JournalVoucher()
                {
                    cash_code = jv2.cash_code,
                    jv_date = jv2.jv_date,
                    coaid = jv2.coaid,
                    debit = jv2.debit,
                    credit = jv2.credit,
                    descriptions = jv2.descriptions,
                    ent_time = jv2.ent_time,
                    shopid = jv2.shopid,
                    RolesId = jv2.RolesId,
                    createddate = jv2.createddate,
                    lastupdateddate = jv2.lastupdateddate,
                };
                db.JournalVouchers.Add(journal);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
