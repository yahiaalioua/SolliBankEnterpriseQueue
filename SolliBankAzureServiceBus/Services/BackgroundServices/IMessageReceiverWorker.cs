namespace SolliBankAzureServiceBus.Services.BackgroundServices
{
    public interface IMessageReceiverWorker
    {
        Task ProcessBankTransactionsMessagesAsync();
    }
}