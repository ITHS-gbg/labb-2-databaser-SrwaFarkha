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

    public void EditBook(string Isbn13, Böcker böcker)
    {
        var book = _dbContext.Böcker.Where(x =>x.Isbn13 == Isbn13).FirstOrDefault();

        if (book != null)
        {
            _dbContext.Böcker.Update(böcker);

            _dbContext.SaveChanges();
        }
    }
   
}