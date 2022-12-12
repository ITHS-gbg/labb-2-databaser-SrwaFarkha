using Bokhandel.Models;
using Bokhandel.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokhandel.DataAccess.Repositories
{
    public class ButikerRepository : IButikerRepository
    {
        private readonly BokhandelContext _dbContext;

        public ButikerRepository()
        {
            _dbContext = new BokhandelContext();
        }

        public List<Butiker> GetAll()
        {
            var butiker = _dbContext.Butiker.ToList();
            return butiker;
        }
    }
}
