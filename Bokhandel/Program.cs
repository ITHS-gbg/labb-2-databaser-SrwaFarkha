using Bokhandel.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    //public static async Task Main()
    //{
    //    var builder = Host.CreateDefaultBuilder();

    //    builder.ConfigureHostConfiguration(icb =>
    //    {
    //        icb.AddUserSecrets<Program>();
    //    });

    //    await builder.Build().RunAsync();
    //}
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
            .AddSingleton<IButikerRepository, ButikerRepository>();
    }
}

public class Executor
{
    private readonly IButikerRepository _butikerRepository;

    public Executor(IButikerRepository butikerRepository)
    {
        _butikerRepository = butikerRepository;
    }

    public void Execute()
    {
        var butiker = _butikerRepository.GetAll();
        foreach (var butik in butiker)
        {
            Console.WriteLine(butik.Namn);
        }
    }
}