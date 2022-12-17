using System.Security.Cryptography.X509Certificates;
using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Model;

namespace Bokhandel.DataAccess.Repositories;

public class BöckerRepository : IBöckerRepository
{
    private readonly BokhandelContext _dbContext;

    public BöckerRepository()
    {
        _dbContext = new BokhandelContext();
    }

    public List<Böcker> GetAll()
    {
        var böcker = _dbContext.Böcker.ToList();
        return böcker;
    }

    public void DeleteBook(string Isbn)
    {
        var book = _dbContext.Böcker.First(x => x.Isbn13 == Isbn);

        _dbContext.Böcker.Remove(book);
        _dbContext.SaveChanges();
    }

    public void EditBook(string isbn13, Böcker updateBöcker)
    {
        var book = _dbContext.Böcker.FirstOrDefault(x => x.Isbn13 == isbn13);

        if (book != null)
        {
            book.Titel = updateBöcker.Titel;
            book.Språk = updateBöcker.Språk;
            book.Pris = updateBöcker.Pris;
            book.Utgivningsdatum = updateBöcker.Utgivningsdatum;
            book.FörfattarId = updateBöcker.FörfattarId;

            _dbContext.Böcker.Update(book);
            _dbContext.SaveChanges();
        }
    }

    public void AddBook(Böcker addBöcker)
    {
        _dbContext.Böcker.Add(addBöcker);
        _dbContext.SaveChanges();
    }
}