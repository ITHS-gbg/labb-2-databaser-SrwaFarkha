using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokhandel
{
    public class BookstoreNavigate
    {
        private readonly IButikerRepository _butikerRepository;
        private readonly ILagerSaldoRepository _lagerSaldoRepository;
        private readonly IBöckerRepository _böckerRepository;

        public BookstoreNavigate(
            IButikerRepository butikerRepository, 
            ILagerSaldoRepository lagerSaldoRepository, 
            IBöckerRepository böckerRepository)
        {
            _butikerRepository = butikerRepository;
            _lagerSaldoRepository = lagerSaldoRepository;
            _böckerRepository = böckerRepository;
        }

        public void BookstoreStartNavigate()
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
                        Console.WriteLine("To book management");
                        break;
                }
            }
        }
    }
}
