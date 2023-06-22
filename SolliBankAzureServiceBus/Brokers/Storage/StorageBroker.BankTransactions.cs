using SolliBankAzureServiceBus.Models.DomainModels;

namespace SolliBankAzureServiceBus.Brokers.Storage
{
    public partial class StorageBroker : IStorageBroker
    {
        private List<BankTransaction> _bankTransactions = new List<BankTransaction>();

        public void Add(BankTransaction bankTransaction)
        {
            _bankTransactions.Add(bankTransaction);
        }
        public void Remove(BankTransaction bankTransaction)
        {
            _bankTransactions.Remove(bankTransaction);
        }
        public List<BankTransaction> GetBankTransactions()
        {
            return _bankTransactions;
        }
    }
}
