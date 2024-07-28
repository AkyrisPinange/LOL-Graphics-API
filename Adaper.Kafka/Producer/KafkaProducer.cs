using Confluent.Kafka;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Kafka.Producer.Producer
{
    public class KafkaProducer : IMessageProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly ILogger<KafkaProducer> _logger;

        public KafkaProducer(IProducer<Null, string> producer, ILogger<KafkaProducer> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        public async Task ProduceAsync(string message)
        {
            try
            {
                var deliveryResult = await _producer.ProduceAsync("test-topic", new Message<Null, string> { Value = message });
                _logger.LogInformation($"Delivered message to {deliveryResult.TopicPartitionOffset}");
            }
            catch (ProduceException<Null, string> e)
            {
                _logger.LogError($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
