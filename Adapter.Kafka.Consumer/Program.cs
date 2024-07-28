using Adapter.Kafka.Consumer.Consumer;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Start consuming events ...");

var builder = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var kafkaConfig = context.Configuration.GetSection("Kafka").GetValue<string>("BootstrapServers");
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = kafkaConfig,
            GroupId = "test-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        services.AddSingleton(new ConsumerBuilder<Ignore, string>(consumerConfig).Build());
        services.AddHostedService<KafkaConsumer>();
    });

var host = builder.Build();
await host.RunAsync();
