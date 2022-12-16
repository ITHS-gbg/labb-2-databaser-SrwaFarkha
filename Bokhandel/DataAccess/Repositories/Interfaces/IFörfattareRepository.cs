using Bokhandel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokhandel.DataAccess.Repositories.Interfaces
{
    public interface IFörfattareRepository
    {
        public List<Författare> GetAll();
        public void AddNewAuthor(Författare författare);

    }
}
