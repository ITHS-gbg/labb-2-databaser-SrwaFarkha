using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.Models;

namespace Bokhandel.DataAccess.Repositories.Interfaces
{
    public interface ILagerSaldoRepository
    {
        public List<LagerSaldo> GetAllStockBalance(int storeId);
    }
}
