using System;
using System.Threading.Tasks;
using MassTransit;

namespace Simple.Consumer
{
    //This is the app that listens and pulls messagaes from the queue.
    class Program
    {
        //Project | Settings | Build | Advanced | c# 7.1 or higher
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                x.ReceiveEndpoint(host, "most_simple_example", e => e.Consumer<PersonConsumer>());

            });

            var handle = await bus.StartAsync();
            Console.WriteLine("I am listening.  Press any key to quit.");
            Console.ReadKey();
            await bus.StopAsync();
        }
    }
}
