using Azure.Messaging.ServiceBus;

namespace SolliBankAzureServiceBus.Brokers.Messaging
{
    public partial interface IQueueBroker
    {
        ServiceBusProcessor CreateProcessor();
    }
}
