using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Models;
using Bokhandel.Repositories.Interfaces;

namespace Bokhandel
{
    public class Bookstore
    {
        //Dependency injection - Hämta data från repository
        private readonly IButikerRepository _butikerRepository;
        private readonly ILagerSaldoRepository _lagerSaldoRepository;
        
        public Bookstore(IButikerRepository butikerRepository, ILagerSaldoRepository lagerSaldoRepository)
        {
            _butikerRepository = butikerRepository;
            _lagerSaldoRepository = lagerSaldoRepository;
        }

        public void BookstoreStartNavigate()
        {
            bool isCountinueNavigate = true;
            while (isCountinueNavigate)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("Choose what you would like to do:");
                Console.WriteLine("1. Go to bookstores");
                Console.WriteLine("2. Add a new title");
                string userInputNav = Console.ReadLine();

                switch (userInputNav)
                {
                    case "1":
                        isCountinueNavigate = false;
                        ChooseBookStore();
                        break;
                    case "2":
                        isCountinueNavigate = false;
                        Console.WriteLine("To add new title method");
                        break;
                }

                break;
            }
        }

        public void ChooseBookStore()
        {
            Console.Clear();
            var bookstores = _butikerRepository.GetAll();

            Console.WriteLine("------------------------------");
            Console.WriteLine("Choose a book store:");
            foreach (var bookstore in bookstores)
            {
                Console.WriteLine($"{bookstore.ButikId}. {bookstore.Namn}");
            }

            int userInputBookstore = Convert.ToInt32(Console.ReadLine());
            foreach (var bookstore in bookstores)
            {
                if (userInputBookstore == bookstore.ButikId)
                {
                    Console.WriteLine($"You have choosen {bookstore.Namn}");
                    BookstoreMainMenu(bookstore);
                }

            }
        }

        public void BookstoreMainMenu(Butiker bookstore)
        {
            Console.Clear();
            Console.WriteLine("------------------------------");
            Console.WriteLine($"You have choosen {bookstore.Namn}");
            Console.WriteLine("------------------------------");
            Console.WriteLine("What do you like to do?");
            Console.WriteLine("1. Show stock balance");
            Console.WriteLine("2. Add or remove books");
            
            string alt = Console.ReadLine();
            switch (alt)
            {
                case "1":
                    ShowStockBalance(bookstore);
                    break;
                case "2":
                    break;
            }
        }

        public void ShowStockBalance(Butiker bookstore)
        {
            Console.Clear();
            Console.WriteLine($"Showing stock balance for {bookstore.Namn}");
            Console.WriteLine("------------------------------");
            var stockBalance = _lagerSaldoRepository.GetAllStockBalance(bookstore.ButikId);

            foreach (var balance in stockBalance)
            {
                var totalPrice = balance.Antal * (int)balance.IsbnNavigation.Pris;
                Console.WriteLine($" Title: '{balance.IsbnNavigation.Titel}' | Amount: {balance.Antal} | Total price: {totalPrice}kr");
            }
            Console.WriteLine($"\n Go back with 'b' ");

            bool isContinueShowStockBalance = true;
            while (isContinueShowStockBalance)
            {
                string back = Console.ReadLine();

                if (back == "b")
                {
                    isContinueShowStockBalance = false;
                    BookstoreMainMenu(bookstore);
                }
            }
           
        }
    }
}
