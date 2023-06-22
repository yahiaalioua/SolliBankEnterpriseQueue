using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;

namespace SolliBankAzureServiceBus.Brokers.Messaging
{
    public partial class QueueBroker
    {
        private readonly IConfiguration _configuration;

        public QueueBroker(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeQueueClient();
        }
        public void InitializeQueueClient()
        {
            BankTransactionsQueue = GetQueueClient();
        }

        private ServiceBusClientOptions GetClientOptions = new ServiceBusClientOptions
        {
            //her kan vi konfigurere options for den service bus client
            RetryOptions = new ServiceBusRetryOptions { MaxRetries = 1 },
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };

        private ServiceBusProcessorOptions GetProcessorOptions = new ServiceBusProcessorOptions
        {
            // her kan vi konfigurere service bus client processor options
            MaxConcurrentCalls = 1,
            //den skal bli satt på falsk slik at vi kan bestemmer når en melding skal bli completed og
            //deretter slettet fra queue. Hvis den står på true det blir gjørt automatisk, men vi kan ikke
            //kontrollere hva blir det gjørt hvis meldingen kan ikke bli prossesere. Det vil si at meldingen
            //blir automatisk slettet.
            AutoCompleteMessages = false
        };

        private ServiceBusClient GetQueueClient()
        {
            string connStr = _configuration.GetConnectionString("SeviceBusConnStr")!;
            return new ServiceBusClient(connStr,GetClientOptions);
        }
    }
}
