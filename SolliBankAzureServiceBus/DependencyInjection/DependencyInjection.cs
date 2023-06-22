using SolliBankAzureServiceBus.Brokers.Messaging;
using SolliBankAzureServiceBus.Brokers.Storage;
using SolliBankAzureServiceBus.Services.BackgroundServices;
using SolliBankAzureServiceBus.Services.FoundationServices;

namespace SolliBankAzureServiceBus.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<IStorageBroker, StorageBroker>();
            services.AddTransient<IQueueBroker, QueueBroker>();
            services.AddTransient<IBankTransactionService, BankTransactionService>();
            services.AddTransient<IMessageReceiverWorker, MessageReceiverWorker>();
            return services;
        }
    }
}
