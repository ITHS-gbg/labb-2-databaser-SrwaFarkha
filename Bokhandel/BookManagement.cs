using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokhandel.Model;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Net;

namespace Bokhandel
{
    public class BookManagement
    {
        private readonly IButikerRepository _butikerRepository;
        private readonly ILagerSaldoRepository _lagerSaldoRepository;
        private readonly IBöckerRepository _böckerRepository;
        private readonly IFörfattareRepository _författareRepository;

        public BookManagement(IButikerRepository butikerRepository, ILagerSaldoRepository lagerSaldoRepository,
            IBöckerRepository böckerRepository, IFörfattareRepository författareRepository)
        {
            _butikerRepository = butikerRepository;
            _lagerSaldoRepository = lagerSaldoRepository;
            _böckerRepository = böckerRepository;
            _författareRepository = författareRepository;
        }
        public void BookManagementStartNavigate()
        {
            bool isCountinue = true;
            while (isCountinue)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine("Choose what you would like to do:");
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. Edit a book");
                Console.WriteLine("3. Add a new author");
                Console.WriteLine("4. Delete a book");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Go back with 'b' ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        isCountinue = false;
                        AddNewBook();
                        break;
                    case "2":
                        isCountinue = false;
                        EditBook();
                        break;
                    case "3":
                        isCountinue = false;
                        AddNewAuthor();
                        break;
                    case "4":
                        isCountinue = false;
                        DeleteBook();
                        break;
                    case "b":
                        isCountinue = false;
                        AppNavigate bookManagement = new AppNavigate(_butikerRepository, _lagerSaldoRepository, _böckerRepository, _författareRepository);
                        bookManagement.AppStartNavigate();
                        break;
                }
            }
        }
        public void AddNewBook()
        {
            bool isContinueAddNewBook = true;
            while (isContinueAddNewBook)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine("You are adding a new book");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Do you want to add a new book with a new author or existing author?"); 
                Console.WriteLine("1. New author");
                Console.WriteLine("2. Existing author");
                Console.WriteLine("3. Want to go back");
                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        isContinueAddNewBook = false;
                        AddNewAuthor();
                        break;
                    case "2":
                        isContinueAddNewBook = false;
                        ExistingAuthorForNewBook(null);
                        break;
                    case "3":
                        isContinueAddNewBook = false;
                        BookManagementStartNavigate();
                        break;
                }
            }
        }
        public void ExistingAuthorForNewBook(Författare författare = null)
        {
            bool isContinueExistingAuthor = true;
            while (isContinueExistingAuthor)
            {
                if (författare != null)
                {
                    StartCreateNewBook(författare);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("Pick the author for your new book");
                    Console.WriteLine("------------------------------");
                    var counter = 1;
                    var authors = _författareRepository.GetAll();
                    var dictionary = new Dictionary<int, Författare>();
                    foreach (var author in authors)
                    {
                        dictionary.Add(counter, author);
                        counter++;
                    }
                    foreach (var author in dictionary)
                    {
                        Console.WriteLine($"{author.Key}. {author.Value.Förnamn} {author.Value.Efternamn}");
                    }
                    var userInput = Console.ReadLine();
                }
                
            }
        }

        public void StartCreateNewBook(Författare författare)
        {
            Console.Clear();
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Creating a new book with the author {författare.Förnamn} {författare.Efternamn}");
            Console.WriteLine("------------------------------");
            Console.WriteLine("wip");
            Console.ReadKey();

        }

        public void EditBook()
        {
            bool isContinueEditBook = true;
            while (isContinueEditBook)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");

                var counter = 1;
                var books = _böckerRepository.GetAll();
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

                Console.WriteLine("\nWhich book would you like to edit?");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Go back with 'b' ");
                var editBook = Console.ReadLine();

                if (editBook == "b")
                {
                    isContinueEditBook = false;
                    BookManagementStartNavigate();
                }

                foreach (var item in dictionary)
                {
                    if (editBook == item.Key.ToString())
                    {
                        
                        Console.Clear();
                        Console.WriteLine("------------------------------");
                        Console.WriteLine($"Isbn13: {item.Value.Isbn13}");
                        Console.WriteLine($"Title: {item.Value.Titel}");
                        Console.WriteLine($"Language: {item.Value.Språk}");
                        Console.WriteLine($"Price: {item.Value.Pris}");
                        Console.WriteLine($"Release date: {item.Value.Utgivningsdatum.ToShortDateString()}");
                        Console.WriteLine($"Author id: {item.Value.FörfattarId}");
                        Console.WriteLine("------------------------------");

                        Console.WriteLine("Are you sure that you want to edit this book? Press 'y' for yes or any key for no and go back");
                        var userInput = Console.ReadLine();
                        Console.WriteLine("------------------------------");


                        if (userInput == "y")
                        {
                            Console.Write("Please enter isbn13:");
                            var userInputIsbn13 = Console.ReadLine();

                            Console.Write("Please enter title:");
                            var userInputTitle = Console.ReadLine();

                            Console.Write("Please enter language:");
                            var userInputLanguage = Console.ReadLine();

                            Console.Write("Please enter price:");
                            int userInputPrice = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Please enter release date:");
                            var userInputReleaseDate = Convert.ToDateTime(Console.ReadLine());

                            Console.Write("Please enter author id:");
                            int userInputAuthorId = Convert.ToInt32(Console.ReadLine());

                            var book = new Böcker
                            {
                                Isbn13 = userInputIsbn13,
                                Titel = userInputTitle,
                                Språk = userInputLanguage,
                                Pris = userInputPrice,
                                Utgivningsdatum = userInputReleaseDate,
                                FörfattarId = userInputAuthorId
                            };
                            var test = item.Value.Isbn13;
                            _böckerRepository.EditBook(item.Value.Isbn13, book);
                        }
                        else 
                        {
                             isContinueEditBook = false;
                             BookManagementStartNavigate();
                        }
                    }
                }
            }
        }

        public void AddNewAuthor()
        {
            bool isContinueAddAuthor = true;
            while (isContinueAddAuthor)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine("You are adding a new author");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Are you sure that you want to add new author? Press 'y' for yes or any key for no and go back");
                var input = Console.ReadLine();
                if (input == "y")
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("You are now adding a new author.");
                    Console.Write("Write firstname: ");
                    var firstName = Console.ReadLine();

                    Console.Write("Write lastname: ");
                    var lastName = Console.ReadLine();

                    Console.Write("Write date of birth: ");
                    var dateOfBirth = Convert.ToDateTime(Console.ReadLine());

                    var newAuthor = new Författare
                    {
                        Förnamn = firstName,
                        Efternamn = lastName,
                        Födelsedatum = dateOfBirth
                    };

                    Console.WriteLine("Congratulations you have created a new author. Would you also like to create a new book with this author? Write any key for adding new book or 'b' for going back.");
                    _författareRepository.AddNewAuthor(newAuthor);
                    var userInput = Console.ReadLine();

                    if (userInput == "b")
                    {
                        isContinueAddAuthor = false;
                        BookManagementStartNavigate();
                    }
                    else
                    {
                        ExistingAuthorForNewBook(newAuthor);
                    }
                }
                else
                {
                    isContinueAddAuthor = false;
                    BookManagementStartNavigate();
                }
            }
        }
        public void DeleteBook()
        {
            bool isContinueDeleteBook = true;
            while (isContinueDeleteBook)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");

                var counter = 1;
                var books = _böckerRepository.GetAll();
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

                Console.WriteLine("\nWhich book would you like to delete?");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Go back with 'b' ");
                var deleteBook = Console.ReadLine();

                if (deleteBook == "b")
                {
                    isContinueDeleteBook = false;
                    BookManagementStartNavigate();
                }

                foreach (var item in dictionary)
                {
                    if (deleteBook == item.Key.ToString())
                    {
                        isContinueDeleteBook = false;
                        _böckerRepository.DeleteBook(item.Value.Isbn13);
                        Console.WriteLine($"Successfully deleted '{item.Value.Titel}'. Press any key to go back.");
                        Console.ReadKey();
                        BookManagementStartNavigate();
                    }
                }
            }
        }
    }
}
