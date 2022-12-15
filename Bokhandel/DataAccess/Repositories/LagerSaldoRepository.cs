using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Model;
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

        public int GetAmountOfTheBook(int butikId, string isbn)
        {
            var amount = _dbContext.LagerSaldo
                .Where(x => x.ButikId == butikId && x.Isbn == isbn)
                .Select(x => x.Antal).First();
            return amount;

        }

        public void AddBook(LagerSaldo lagersaldo)
        {
            var stockBalance = _dbContext.LagerSaldo.Where(x => x.ButikId == lagersaldo.ButikId).ToList();

            var isNewBook = stockBalance.Any(x => x.Isbn == lagersaldo.Isbn);

            if (!isNewBook)
            {
                _dbContext.LagerSaldo.Add(lagersaldo);
            }
            else
            {
                stockBalance.First(x => x.Isbn == lagersaldo.Isbn).Antal += lagersaldo.Antal;
            }

            _dbContext.SaveChanges();
        }

        public void RemoveBook(LagerSaldo lagersaldo)
        {
            var stockBalance = _dbContext.LagerSaldo.Where(x => x.ButikId == lagersaldo.ButikId).ToList();

            var existingBook = stockBalance.Any(x => x.Isbn == lagersaldo.Isbn);
            if (existingBook)
            {
                stockBalance.First(x => x.Isbn == lagersaldo.Isbn).Antal -= lagersaldo.Antal;
            }

            _dbContext.SaveChanges();
            // _dbContext.LagerSaldo.Remove(lagersaldo);



        }
    }
}
