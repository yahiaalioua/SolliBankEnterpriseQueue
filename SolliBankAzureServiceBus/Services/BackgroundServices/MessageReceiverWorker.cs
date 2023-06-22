using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SolliBankAzureServiceBus.Brokers.Messaging;
using SolliBankAzureServiceBus.Brokers.Storage;
using SolliBankAzureServiceBus.Models.DomainModels;
using System.Text;
using System.Text.Json;

namespace SolliBankAzureServiceBus.Services.BackgroundServices
{
    public class MessageReceiverWorker : IMessageReceiverWorker
    {
        private readonly ILogger<MessageReceiverWorker> _logger;    
        private readonly IQueueBroker _queueBroker;
        private readonly IStorageBroker _storageBroker;

        public MessageReceiverWorker(
            IQueueBroker queueBroker,
            IStorageBroker storageBroker,
            ILogger<MessageReceiverWorker> logger
            )
        {
            _queueBroker = queueBroker;
            _storageBroker = storageBroker;
            _logger = logger;
        }

        public async Task ProcessBankTransactionsMessagesAsync()
        {
            _logger.LogInformation($"Starting processing messages from the queue,Date:{DateTime.UtcNow}");
            var processor = _queueBroker.CreateProcessor();            
            processor.ProcessMessageAsync += HandleMessageAsync;
            processor.ProcessErrorAsync += HandleErrorAsync;
            await processor.StartProcessingAsync();
        }
        private async Task HandleMessageAsync(ProcessMessageEventArgs messageEventArgs)
        {
            try
            {
                _logger.LogInformation($"Mapping message to a bank transaction and adding to database, Date:{DateTime.UtcNow}");
                _storageBroker.Add(JsonConvert.DeserializeObject<BankTransaction>(messageEventArgs.Message.Body.ToString()));
                // etter at vi har behandlet vårt melding da kan vi gi beskjed til queue at melding
                // er behandlet og deretter skal slettes slik at ingen andre kan behandle det på nytt
                await messageEventArgs.CompleteMessageAsync(messageEventArgs.Message);
                _logger.LogInformation($"Message processed succesfully, the message will be removed from the queue, Date:{DateTime.UtcNow}");
            }
            catch (Exception)
            {
                // hvis det er oppstått en feil og vi kan ikke behandle meldingen, da kan vi veldger å løse opp meldingen,
                //og la en annen forbrukerer prøve på nytt, eller vi kan flytter meldingen til dead letter queue,
                //eller vi kan defferre melding slik at den sitter ved side og kan bli behandlet på nytt
                _logger.LogInformation($"Message could not be processed, the message will be forwarded to the dead letter, Date:{DateTime.UtcNow}");
                await messageEventArgs.DeadLetterMessageAsync(messageEventArgs.Message);
                _logger.LogInformation($"Message succesfully forwarded to the dead letter, Date:{DateTime.UtcNow}");
            }
        }

        private Task HandleErrorAsync(ProcessErrorEventArgs errorEventArgs)
        {
            _logger.LogError($"error: an error has occured while processing the message from the queue,{errorEventArgs.Exception},Date:{DateTime.UtcNow}");
            return Task.CompletedTask;
        }
    }
}
