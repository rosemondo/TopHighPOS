using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain;

namespace TopHighPos.IBusiness.JournalVoucherInterface
{
    public interface IJournalVoucherRepository
    {
        void AddnewJournal1(JournalVoucherModel jv);
        void AddnewJournal2(JournalVoucherModel jv2);
    }
}
