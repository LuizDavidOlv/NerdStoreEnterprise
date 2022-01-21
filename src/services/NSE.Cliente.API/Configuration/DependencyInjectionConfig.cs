﻿using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSE.Cliente.API.Application.Commands;
using NSE.Cliente.API.Data;
using NSE.Cliente.API.Data.Repository;
using NSE.Cliente.API.Models;
using NSE.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ClientesContext>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();
        }
    }
}