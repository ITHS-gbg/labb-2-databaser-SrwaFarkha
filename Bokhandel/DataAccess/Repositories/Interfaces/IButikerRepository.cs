using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.Models;

namespace Bokhandel.Repositories.Interfaces
{
    public interface IButikerRepository
    {
        public List<Butiker> GetAll();
    }
}
