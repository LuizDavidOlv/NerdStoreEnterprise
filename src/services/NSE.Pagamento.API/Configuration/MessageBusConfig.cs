using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.Core.Utils;
using NSE.MessageBus;
using NSE.Pagamento.API.Service;

namespace NSE.Pagamento.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("KafkaBus"))
                .AddHostedService<PagamentoIntegrationHandler>();
        }
    }
}
