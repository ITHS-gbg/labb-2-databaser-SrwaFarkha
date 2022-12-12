using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Bokhandel.DataAccess.Repositories
{
    public class LagerSaldoRepository: ILagerSaldoRepository
    {
        private readonly BokhandelContext _dbContext;

        public LagerSaldoRepository()
        {
            _dbContext = new BokhandelContext();
        }

        public List<LagerSaldo> GetAllStockBalance(int storeId)
        {
            var stockBalance = _dbContext.LagerSaldo
                .Where(x=> x.ButikId == storeId)
                .Include(x=>x.IsbnNavigation)
                .ToList();
            return stockBalance;
        }

        public void AddBook(Butiker butik, Böcker book, LagerSaldo lagersaldo)
        {
            var newBook = _dbContext.LagerSaldo.Add(lagersaldo);
        }
    }
}
