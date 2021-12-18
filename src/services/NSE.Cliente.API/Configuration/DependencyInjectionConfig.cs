using Microsoft.Extensions.DependencyInjection;
using NSE.Cliente.API.Data;
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
        }
    }
}
