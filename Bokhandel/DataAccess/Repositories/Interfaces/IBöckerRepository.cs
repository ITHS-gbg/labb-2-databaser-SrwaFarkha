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
    }
}
