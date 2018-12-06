using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Services.Interfaces;
using RabbitMQ.Client;

namespace CapstoneApp.Shared.Services.Implementations
{
    public class MessagingService : IMessagingService
    {
        private ConnectionFactory _factory;

        public async Task SendConfig(MirrorConfig config)
        {
            try
            {
                _factory = new ConnectionFactory() { HostName = config.Mirror.IpAddress };
                _factory.UserName = "config";
                _factory.Password = "cfg_user";
                _factory.VirtualHost = "/";
                using (var connection = _factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "config_settings",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        var cfgString = Newtonsoft.Json.JsonConvert.SerializeObject(config);
                        var cfgBytes = Encoding.UTF8.GetBytes(cfgString);
                        channel.BasicPublish(exchange: "", 
                            routingKey: "config_settings",
                            basicProperties: null,
                            body: cfgBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
