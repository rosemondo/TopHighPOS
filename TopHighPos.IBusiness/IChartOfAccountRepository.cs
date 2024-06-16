using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain;

namespace TopHighPos.IBusiness.ChartOfAccountInterface
{
    public interface IChartOfAccountRepository
    {
        List<ChartOfAccounts> GetAllAccounts();
    }
}
