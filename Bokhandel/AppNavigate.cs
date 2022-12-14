using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.Model;

namespace Bokhandel
{
    public class AppNavigate
    {
        private readonly IButikerRepository _butikerRepository;
        private readonly ILagerSaldoRepository _lagerSaldoRepository;
        private readonly IBöckerRepository _böckerRepository;
        private readonly IFörfattareRepository _författareRepository;



        public AppNavigate(
            IButikerRepository butikerRepository, 
            ILagerSaldoRepository lagerSaldoRepository, 
            IBöckerRepository böckerRepository,
            IFörfattareRepository författareRepository)

        {
            _butikerRepository = butikerRepository;
            _lagerSaldoRepository = lagerSaldoRepository;
            _böckerRepository = böckerRepository;
            _författareRepository = författareRepository;

        }

        public void AppStartNavigate()
        {
            bool isCountinueNavigate = true;
            while (isCountinueNavigate)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine("Choose what you would like to do:");
                Console.WriteLine("1. Go to bookstores");
                Console.WriteLine("2. Go to book management");
                string userInputNav = Console.ReadLine();

                switch (userInputNav)
                {
                    case "1":
                        isCountinueNavigate = false;
                        Bookstore bookstore = new Bookstore(_butikerRepository, _lagerSaldoRepository, _böckerRepository);
                        bookstore.ChooseBookStore();
                        break;
                    case "2":
                        isCountinueNavigate = false;
                        BookManagement bookManagement = new BookManagement(_butikerRepository, _lagerSaldoRepository, _böckerRepository,_författareRepository);
                        bookManagement.BookManagementStartNavigate();
                        break;
                }
            }
        }
    }
}
