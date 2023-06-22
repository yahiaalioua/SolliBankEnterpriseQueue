using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;

namespace SolliBankAzureServiceBus.Brokers.Messaging
{
    public partial interface IQueueBroker
    {
        Task SendMessage(ServiceBusMessage message);
    }
}
