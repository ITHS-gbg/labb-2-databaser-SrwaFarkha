using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.Model;

namespace Bokhandel.DataAccess.Repositories.Interfaces
{
    public interface IBöckerRepository
    {
        public List<Böcker> GetAll();
        public void DeleteBook(string Isbn);
        public void EditBook(string Isbn13, Böcker updateBöcker);
        public void AddBook(Böcker addBöcker);
    }
}
