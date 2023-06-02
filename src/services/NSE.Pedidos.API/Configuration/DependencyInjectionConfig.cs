using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSE.Core.Http;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.Pedidos.API.Application.Commands;
using NSE.Pedidos.API.Application.Events;
using NSE.Pedidos.API.Application.Queries;
using NSE.Pedidos.Domain.Pedidos;
using NSE.Pedidos.Domain.Vouchers;
using NSE.Pedidos.Infra.Data;
using NSE.Pedidos.Infra.Data.Repository;
using NSE.WebApi.Core.Usuario;

namespace NSE.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IRestClient, RestClient>();
            services.AddHttpClient(nameof(PedidoIniciadoIntegrationEvent), options =>
            {
                options.BaseAddress = new System.Uri("https://localhost:5601/pagamento/pedido-iniciado");
            });
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            // Commands
            services.AddScoped<IRequestHandler<AdicionarPedidoCommand, ValidationResult>, PedidoCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<PedidoRealizadoEvent>, PedidoEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVoucherQueries, VoucherQueries>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            // Data
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<PedidosContext>();
        }
    }
}