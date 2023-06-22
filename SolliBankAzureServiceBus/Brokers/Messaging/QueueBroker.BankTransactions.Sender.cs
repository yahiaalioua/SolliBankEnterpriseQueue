using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;

namespace SolliBankAzureServiceBus.Brokers.Messaging
{
    public partial class QueueBroker:IQueueBroker
    {
        public string BankTransactionsQueueName { get; } = "banktransactionsqueue";
        public ServiceBusClient BankTransactionsQueue { get; set; }

        private ServiceBusSender CreateSender()=>
            BankTransactionsQueue.CreateSender(BankTransactionsQueueName);
        public async Task SendMessage(ServiceBusMessage message)
        {
            var sender = CreateSender();
            await sender.SendMessageAsync(message);
        }
    }
}
