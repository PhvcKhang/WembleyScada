using Microsoft.AspNetCore.SignalR.Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        HubConnection connection = new HubConnectionBuilder()
        .WithUrl(new Uri("https://localhost:7125/NotificationHub"))
        .Build();

        connection.On<string>("OnTagChanged", (str) =>
        {
            Console.WriteLine(str);
        });
        connection.On<string>("LogInfoMessage", (str) =>
        {
            Console.WriteLine(str);
        });

        await connection.StartAsync();

        await connection.InvokeAsync("UpdateTopics", new List<string>() { "Wembley/HerapinCap/IE-F2-HCA01/S1/out" });

        Console.ReadLine();

    }
}