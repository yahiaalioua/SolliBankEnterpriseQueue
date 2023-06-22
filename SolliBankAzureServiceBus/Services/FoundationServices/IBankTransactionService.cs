using SolliBankAzureServiceBus.Models.DomainModels;

namespace SolliBankAzureServiceBus.Services.FoundationServices
{
    public interface IBankTransactionService
    {
        Task AddBankTransaction(BankTransaction transaction);
        List<BankTransaction> GetBankTransactions();
    }
}