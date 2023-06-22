using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace SolliBankAzureServiceBus.Brokers.Messaging
{
    public partial class QueueBroker:IQueueBroker
    {
        public ServiceBusProcessor CreateProcessor() =>
            BankTransactionsQueue.CreateProcessor(BankTransactionsQueueName,GetProcessorOptions);

    }
}
