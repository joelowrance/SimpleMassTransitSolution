using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.Contracts;

namespace Simple.Consumer
{
    class PersonConsumer : IConsumer<IPerson>
    {
        public Task Consume(ConsumeContext<IPerson> context)
        {
            Console.WriteLine($"CONSUMED:  {context.Message.Name}");
            return Task.FromResult(0); //We aren't doing any async here.
        }
    }
}
