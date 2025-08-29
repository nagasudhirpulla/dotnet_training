using Microsoft.Extensions.DependencyInjection;

//setup DI container
var serviceProvider = new ServiceCollection()
    .AddSingleton<IMyService, MyService>()
    .BuildServiceProvider();

//get the service from DI container and use it
var srv = serviceProvider.GetService<IMyService>();
srv?.DoSomething();

Console.WriteLine("Completed!");

public interface IMyService
{
    void DoSomething();
}

public class MyService : IMyService
{
    public void DoSomething()
    {
        Console.WriteLine("MyService is doing something.");
    }
}
