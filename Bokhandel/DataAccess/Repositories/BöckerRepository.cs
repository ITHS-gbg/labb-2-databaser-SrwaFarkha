using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Models;

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

}