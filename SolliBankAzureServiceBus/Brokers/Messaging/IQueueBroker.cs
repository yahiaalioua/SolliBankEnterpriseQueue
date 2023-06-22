using Microsoft.Azure.ServiceBus;

namespace SolliBankAzureServiceBus.Brokers.Messaging
{
    public partial interface IQueueBroker
    {
        void InitializeQueueClient();
    }
}