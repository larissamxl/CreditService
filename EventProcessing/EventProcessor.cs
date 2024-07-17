using AutoMapper;
using CreditService.Data;
using CreditService.Dtos;
using CreditService.Models;
using System.Text.Json;

namespace CreditService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.CustomerPublished:
                    AddCustomer(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch (eventType.Event)
            {
                case "Customer_Published":
                    Console.WriteLine("--> Customer Published Event Detected");
                    return EventType.CustomerPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void AddCustomer(string customerPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICreditRepo>();

                var customerPublishedDto = JsonSerializer.Deserialize<CustomerPublishedDto>(customerPublishedMessage);

                try
                {
                    var customer = _mapper.Map<Customer>(customerPublishedDto);
                    if (!repo.CustomerExists(customer.IdCustomer))
                    {
                        repo.CreateCustomer(customer);
                        repo.SaveChanges();
                        Console.WriteLine("--> Customer added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Customer already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Customer to DB {ex.Message}");
                }
            }
        }

        enum EventType
        {
            CustomerPublished,
            Undetermined
        }
    }
}
