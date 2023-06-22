using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using SolliBankAzureServiceBus.Brokers.Messaging;
using SolliBankAzureServiceBus.Brokers.Storage;
using SolliBankAzureServiceBus.Models.DomainModels;
using System.Transactions;

namespace SolliBankAzureServiceBus.Services.FoundationServices
{
    public class BankTransactionService : IBankTransactionService
    {
        private ILogger<BankTransactionService> _logger;
        private readonly IQueueBroker _queueBroker;
        private readonly IStorageBroker _storageBroker;

        public BankTransactionService(
            IQueueBroker queueBroker,
            IStorageBroker storageBroker,
            ILogger<BankTransactionService> logger
            )
        {
            _queueBroker = queueBroker;
            _storageBroker = storageBroker;
            _logger = logger;
        }

        public async Task AddBankTransaction(BankTransaction transaction)
        {
            _logger.LogInformation($"Starting {nameof(AddBankTransaction)}, DateTime:{DateTime.UtcNow}");
            var sbMessage = new ServiceBusMessage(JsonConvert.SerializeObject(transaction));
            //her setter vi header properties for vårt service bus melding
            sbMessage.MessageId = transaction.TransactionId.ToString();
            //Subject kan vi bruker for å filtrere våre meldingene ;)
            sbMessage.Subject = "new bank transaction";
            //her kan vi setter andre relevant message fields i vårt sbMessage
            sbMessage.ApplicationProperties["TransactionType"] = transaction.TransactionType;
            await _queueBroker.SendMessage(sbMessage);
            _logger.LogInformation($"Transaction sent to the queue");
        }

        public List<BankTransaction> GetBankTransactions()
        {
            return _storageBroker.GetBankTransactions();
        }
    }
}
