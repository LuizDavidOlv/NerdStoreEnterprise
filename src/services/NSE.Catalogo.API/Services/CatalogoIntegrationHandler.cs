﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Catalogo.API.Models;
using NSE.Core.DomainObjects;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Services
{
    public class CatalogoIntegrationHandler : BackgroundService
    {
        //private readonly IMessageBus _bus;
        private readonly IKafkaBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CatalogoIntegrationHandler(IKafkaBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers(stoppingToken);
            return Task.CompletedTask;
        }

        private void SetSubscribers(CancellationToken stoppingToken)
        {
            _bus.ConsumerAsync<PedidoAutorizadoIntegrationEvent>
                ("PeidoAutorizado", async request => await BaixarEstoque(request), stoppingToken);
        }

        private async Task BaixarEstoque(PedidoAutorizadoIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var produtosComEstoque = new List<Produto>();
                var produtoRepository = scope.ServiceProvider.GetRequiredService<IProdutoRepository>();

                var idsProdutos = string.Join(",", message.Itens.Select(c => c.Key));
                var produtos = await produtoRepository.ObterProdutosPorId(idsProdutos);

                if (produtos.Count != message.Itens.Count)
                {
                    CancelarPedidoSemEstoque(message);
                    return;
                }

                foreach (var produto in produtosComEstoque)
                {
                    produtoRepository.Atualizar(produto);
                }

                if (!await produtoRepository.UnitOfWork.Commit())
                {
                    throw new DomainException($"Problemas ao atualizar estoque do pedido {message.PedidoId}");
                }

                var pedidoBaixado = new PedidoBaixadoEstoqueIntegrationEvent(message.ClienteId, message.PedidoId);
                await _bus.ProducerAsync("PedidoBaixadoEstoque", pedidoBaixado);
            }
        }
        private async void CancelarPedidoSemEstoque(PedidoAutorizadoIntegrationEvent message)
        {
            var pedidoCancelado = new PedidoCanceladoIntegrationEvent(message.ClienteId, message.PedidoId);
            await _bus.ProducerAsync("PedidoCancelado",pedidoCancelado);
        }
    }
}
