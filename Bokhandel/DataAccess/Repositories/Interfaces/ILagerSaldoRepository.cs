using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.Model;

namespace Bokhandel.DataAccess.Repositories.Interfaces
{
    public interface ILagerSaldoRepository
    {
        public List<LagerSaldo> GetAllStockBalance(int storeId);
        public void AddBook(LagerSaldo lagersaldo);
        public void RemoveBook(LagerSaldo lagersaldo);
        public int GetAmountOfTheBook(int butikId, string isbn);


    }
}
