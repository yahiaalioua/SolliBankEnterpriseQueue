using Serilog;
using SolliBankAzureServiceBus.Brokers.Messaging;
using SolliBankAzureServiceBus.Brokers.Storage;
using SolliBankAzureServiceBus.DependencyInjection;
using SolliBankAzureServiceBus.Services.BackgroundServices;
using SolliBankAzureServiceBus.Services.FoundationServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.GetService<IMessageReceiverWorker>()!.ProcessBankTransactionsMessagesAsync();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
