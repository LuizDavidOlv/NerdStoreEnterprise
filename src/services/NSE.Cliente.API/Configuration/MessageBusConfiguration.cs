﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.Cliente.API.Services;
using NSE.Core.Utils;
using NSE.MessageBus;

namespace NSE.Cliente.API.Configuration
{
    public static class MessageBusConfiguration
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMessageBus(configuration.GetMessageQueueConnection("KafkaBus"))
            //    .AddHostedService<RegistroClienteIntegrationHandler>();
        }
    }
}
