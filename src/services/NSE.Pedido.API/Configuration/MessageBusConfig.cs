﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.Core.Utils;
using NSE.MessageBus;

namespace NSE.Pedido.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfigutation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
        }
    }
}
