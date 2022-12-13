using Bokhandel;
using Bokhandel.DataAccess.Repositories;
using Bokhandel.DataAccess.Repositories.Interfaces;
using Bokhandel.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        services
            .AddSingleton<Executor, Executor>()
            .BuildServiceProvider()
            .GetService<Executor>()
            .Execute();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton<IButikerRepository, ButikerRepository>()
            .AddSingleton<ILagerSaldoRepository, LagerSaldoRepository>()
            .AddSingleton<IBöckerRepository, BöckerRepository>();

    }
}

public class Executor
{
    private readonly IButikerRepository _butikerRepository;
    private readonly ILagerSaldoRepository _lagerSaldoRepository;
    private readonly IBöckerRepository _böckerRepository;



    public Executor(
        IButikerRepository butikerRepository, 
        ILagerSaldoRepository lagerSaldoRepository, 
        IBöckerRepository böckerRepository)
    {
        _butikerRepository = butikerRepository;
        _lagerSaldoRepository = lagerSaldoRepository;
        _böckerRepository = böckerRepository;
    }


    public void Execute()
    {
        BookstoreNavigate bookstore = new BookstoreNavigate(_butikerRepository, _lagerSaldoRepository, _böckerRepository);
        bookstore.BookstoreStartNavigate();
    }
}