namespace SolliBankAzureServiceBus.Models.DomainModels
{
    public class BankTransaction
    {
        public Guid TransactionId { get; private set; } = Guid.NewGuid();
        public DateTime DateTime { get; private set; } = DateTime.UtcNow;
        public string TransactionType { get; init; } = null!;
        public string SenderAccountNumber { get; init; } = null!;
        public string ReceiverAccountNumber { get; init; }=null!;
        public double Amount { get; init; }
    }
}
