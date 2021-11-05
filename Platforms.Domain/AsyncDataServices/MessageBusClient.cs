using Microsoft.Extensions.Configuration;
using Platforms.Domain.DTOs;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace Platforms.Domain.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMqHost"],
                Port = int.Parse(_configuration["RabbitMqPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMq_ConnectionShutdown;

                Console.WriteLine($"Connected to RabbitMq");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not connect to RabbitMq: {ex.Message}");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
        }

        private void RabbitMq_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"ConnectionShutdown RabbitMq");
        }

        public void PublishNewPlatform(PlatformPublishedDto platform)
        {
            var message = JsonSerializer.Serialize(platform);

            if(_connection.IsOpen)
            {
                Console.WriteLine($"RabbitMq connection open, sending message");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine($"RabbitMq connection closed");
            }
        }
    }
}
