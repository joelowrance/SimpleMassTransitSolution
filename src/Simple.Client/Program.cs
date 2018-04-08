using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.Contracts;

namespace Simple.Client
{

    //This is the app that publishes the messages
    public class Program
    {
        //Project | Settings | Build | Advanced | c# 7.1 or higher
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), hostConfig =>
                {
                    hostConfig.Username("guest");
                    hostConfig.Password("guest");
                });
            });

            var handle = await bus.StartAsync();
            var input = string.Empty;

            while (input != "q")
            {
                Console.Write("Enter person's name or `q` to quit: ");
                input = Console.ReadLine();
                var message = new Person {Name = input};
                Console.WriteLine($"Sending person: {input}");
                await bus.Publish<IPerson>(message);
            }

            await handle.StopAsync();
        }
    }
}
