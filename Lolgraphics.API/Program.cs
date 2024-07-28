using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Adapter.Kafka.Producer.Producer;
using Adapter.Redis.Service;
using Microsoft.Extensions.Options;
using Adapter.Mongo;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // This line is important for Swagger
builder.Services.AddSwaggerGen();

// Kafka Producer configuration
var kafkaConfig = builder.Configuration.GetSection("Kafka").GetValue<string>("BootstrapServers");
var producerConfig = new ProducerConfig
{
    BootstrapServers = kafkaConfig,
    Acks = Acks.All,
    // Add other producer settings if needed
};

// Register the Kafka Producer
builder.Services.AddSingleton<IProducer<Null, string>>(provider =>
    new ProducerBuilder<Null, string>(producerConfig).Build());

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Configure MongoDB settings
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

// Add MongoDB client and context
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddSingleton<MongoDbContext>();


// Register KafkaProducer and other services
builder.Services.AddTransient<KafkaProducer>();
builder.Services.AddTransient<RedisService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lolgraphics API v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.Run();
