using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Model;

namespace Bokhandel.DataAccess.Repositories
{
    public class FörfattareRepository:IFörfattareRepository
    {
        private readonly BokhandelContext _dbContext;
        public FörfattareRepository()
        {
            _dbContext = new BokhandelContext();
        }
        public List<Författare> GetAll()
        {
            var författare = _dbContext.Författare.ToList();
            return författare;
        }
        public void AddNewAuthor(Författare författare)
        {
            _dbContext.Författare.Add(författare);
            _dbContext.SaveChanges();
        }

        public Författare GetById(int id)
        {
            var author = _dbContext.Författare.FirstOrDefault(x => x.Id == id);
            return author;
        }
    }
}
