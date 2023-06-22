using SolliBankAzureServiceBus.Models.DomainModels;

namespace SolliBankAzureServiceBus.Brokers.Storage
{
    public interface IStorageBroker
    {
        void Add(BankTransaction bankTransaction);
        List<BankTransaction> GetBankTransactions();
        void Remove(BankTransaction bankTransaction);
    }
}