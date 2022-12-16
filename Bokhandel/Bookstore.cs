using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Model;
using Bokhandel.Repositories.Interfaces;
using static System.Reflection.Metadata.BlobBuilder;

namespace Bokhandel
{
    public class Bookstore
    {
        //Dependency injection - Hämta data från repository
        private readonly IButikerRepository _butikerRepository;
        private readonly ILagerSaldoRepository _lagerSaldoRepository;
        private readonly IBöckerRepository _böckerRepository;
        private readonly IFörfattareRepository _författareRepository;

        public Bookstore(IButikerRepository butikerRepository, ILagerSaldoRepository lagerSaldoRepository,
            IBöckerRepository böckerRepository)
        {
            _butikerRepository = butikerRepository;
            _lagerSaldoRepository = lagerSaldoRepository;
            _böckerRepository = böckerRepository;
        }
        public void ChooseBookStore()
        {
            bool isContinueChooseBookstore = true;
            while (isContinueChooseBookstore)
            {
                Console.Clear();
                var bookstores = _butikerRepository.GetAll();

                Console.WriteLine("------------------------------");
                Console.WriteLine("Choose a book store:");
                bookstores.ForEach(x => Console.WriteLine($"{x.ButikId}. {x.Namn}"));
                Console.WriteLine("------------------------------");
                Console.WriteLine("Go back with 'b' ");
                string userInputBookstore = Console.ReadLine();
                
                if (userInputBookstore == "b")
                {
                    isContinueChooseBookstore = false;
                    AppNavigate bookstoreNavigate = new AppNavigate(_butikerRepository, _lagerSaldoRepository, _böckerRepository, _författareRepository);
                    bookstoreNavigate.AppStartNavigate();
                }

                foreach (var bookstore in bookstores)
                {
                    if (userInputBookstore == bookstore.ButikId.ToString())
                    {
                        isContinueChooseBookstore = false;
                        Console.WriteLine($"You have choosen {bookstore.Namn}");
                        BookstoreMainMenu(bookstore);
                    }
                }
            }
        }
        public void BookstoreMainMenu(Butiker bookstore)
        {
            bool isContinueShowMainMenu = true;
            while (isContinueShowMainMenu)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine($"You have choosen {bookstore.Namn}");
                Console.WriteLine("------------------------------");
                Console.WriteLine("What do you like to do?");
                Console.WriteLine("1. Show stock balance");
                Console.WriteLine("2. Add or remove books");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Go back with 'b' ");

                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        isContinueShowMainMenu = false;
                        ShowStockBalance(bookstore);
                        break;
                    case "2":
                        isContinueShowMainMenu = false;
                        AddOrRemoveBooks(bookstore);
                        break;
                    case "b":
                        isContinueShowMainMenu = false;
                        ChooseBookStore();
                        break;
                }
            }
        }
        public void ShowStockBalance(Butiker bookstore)
        {
            Console.Clear();
            Console.WriteLine($"Showing stock balance for {bookstore.Namn}");
            Console.WriteLine("------------------------------");
            var stockBalance = _lagerSaldoRepository.GetAllStockBalance(bookstore.ButikId);

            stockBalance.ForEach(x => 
                Console.WriteLine($" Title: '{x.IsbnNavigation.Titel}' | Amount: {x.Antal} | Total price: {x.Antal * (int)x.IsbnNavigation.Pris}kr"));

            Console.WriteLine("\n Go back with 'b' ");

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
        public void AddOrRemoveBooks(Butiker bookstore)
        {
            bool isContinueAddOrRemove = true;
            while (isContinueAddOrRemove)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine($"You are in {bookstore.Namn}");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Do you like to add or remove books?");
                Console.WriteLine("1. Add books");
                Console.WriteLine("2. Remove books");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Go back with 'b' ");
                string addOrRemove = Console.ReadLine();

                switch (addOrRemove)
                {
                    case "1":
                        isContinueAddOrRemove = false;
                        AddBook(bookstore);
                        break;
                    case "2":
                        isContinueAddOrRemove = false;
                        RemoveBook(bookstore);
                        break;
                    case "b":
                        isContinueAddOrRemove = false;
                        BookstoreMainMenu(bookstore);
                        break;
                }
            }
        }
        public void AddBook(Butiker bookstore)
        {
            var books = _böckerRepository.GetAll();
            bool isContinueAddingBooks = true;
            while (isContinueAddingBooks)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine($"You are adding books in {bookstore.Namn} ");
                Console.WriteLine("------------------------------");
                int counter = 1;

                var dictionary = new Dictionary<int, Böcker>();
                foreach (var book in books)
                {
                    dictionary.Add(counter, book);
                    counter++;
                }
                foreach (var book in dictionary)
                {
                    Console.WriteLine($"{book.Key}. {book.Value.Titel}");
                }
                Console.WriteLine("\nPick the book you would like to add: ");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Go back with 'b' ");
                string userInput = Console.ReadLine();

                bool isBookExist = dictionary.Any(x => x.Key.ToString() == userInput);
                if (userInput == "b")
                {
                    AddOrRemoveBooks(bookstore);
                }
                if (isBookExist)
                {
                    Console.WriteLine("How many of this book would you like to add?");
                    int amountBookToAdd;

                    while (!int.TryParse(Console.ReadLine(), out amountBookToAdd))
                        Console.WriteLine("You entered an invalid number, please enter a valid number");
                    
                    Console.WriteLine("Thank you, we will add the book you picked.");

                    foreach (var book in dictionary)
                    {
                        if (userInput == book.Key.ToString())
                        {
                            var lagersaldo = new LagerSaldo
                            {
                                ButikId = bookstore.ButikId,
                                Isbn = book.Value.Isbn13,
                                Antal = amountBookToAdd
                            };

                            _lagerSaldoRepository.AddBook(lagersaldo);
                        }
                    }

                    Console.WriteLine("Would you like to add more? y/n");
                    string addMoreBooks = Console.ReadLine();
                    if (addMoreBooks == "y")
                        continue;
                    else
                    {
                        isContinueAddingBooks = false;
                        BookstoreMainMenu(bookstore);
                    }
                }
                else
                {
                    Console.WriteLine("There is no valid book for that key, please press any key to try again");
                    Console.ReadKey();
                }
            }
        }
        public void RemoveBook(Butiker bookstore)
        {
            var books = _lagerSaldoRepository.GetAllStockBalance(bookstore.ButikId);
            bool isContinueRemovingBooks = true;
            while (isContinueRemovingBooks)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine($"You are removing books in {bookstore.Namn} ");
                Console.WriteLine("------------------------------");
                int counter = 1;

                var dictionary = new Dictionary<int, LagerSaldo>();
                foreach (var book in books)
                {
                    dictionary.Add(counter, book);
                    counter++;
                }

                foreach (var book in dictionary)
                {
                    Console.WriteLine($"{book.Key}. {book.Value.IsbnNavigation.Titel}");
                }

                Console.WriteLine("\nPick the book you would like to remove: ");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Go back with 'b' ");
                string userInput = Console.ReadLine();
                var amount = 0;
                var title = "";
                if (userInput == "b")
                {
                    AddOrRemoveBooks(bookstore);
                }
                foreach (var book in dictionary)
                {
                    if (userInput == book.Key.ToString())
                    {
                        amount = _lagerSaldoRepository.GetAmountOfTheBook(bookstore.ButikId, book.Value.IsbnNavigation.Isbn13);
                        title = book.Value.IsbnNavigation.Titel;
                    }
                }
                bool isBookExist = dictionary.Any(x => x.Key.ToString() == userInput);
                if (isBookExist)
                {
                    Console.WriteLine($"We have {amount} books of '{title}' in stock");
                    Console.WriteLine($"How many of '{title}' would you like to remove?");

                    int amountBookToRemove;

                    while (!int.TryParse(Console.ReadLine(), out amountBookToRemove))
                    {
                        Console.WriteLine("You entered an invalid number, please enter a valid number");
                    }
                    if (amountBookToRemove > amount)
                    {
                        Console.WriteLine("We don't have so many books in stock to remove, please press any key to try again.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Thank you, we will remove the book you picked.");
                        foreach (var book in dictionary)
                        {
                            if (userInput == book.Key.ToString())
                            {
                                var lagersaldo = new LagerSaldo
                                {
                                    ButikId = bookstore.ButikId,
                                    Isbn = book.Value.IsbnNavigation.Isbn13,
                                    Antal = amountBookToRemove
                                };
                                _lagerSaldoRepository.RemoveBook(lagersaldo);
                            }
                        }
                        Console.WriteLine("Would you like to remove more? y/n");
                        string removeMoreBooks = Console.ReadLine();
                        if (removeMoreBooks == "y")
                            continue;
                        else
                        {
                            isContinueRemovingBooks = false;
                            BookstoreMainMenu(bookstore);
                        }
                    } 
                }
                else
                {
                    Console.WriteLine("There is no valid book for that key, please press any key to try again");
                    Console.ReadKey();
                }
            }
        }
    }
}
