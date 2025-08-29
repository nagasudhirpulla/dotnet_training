namespace DIWebAppDemo.Services;

public class MessageWriter : IMessageWriter
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}

public static class MessageWriterExtensions
{
    public static IServiceCollection AddMessageWriter(this IServiceCollection services)
    {
        services.AddSingleton<IMessageWriter, MessageWriter>();
        return services;
    }
}
