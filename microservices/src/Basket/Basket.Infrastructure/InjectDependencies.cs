using Basket.Infrastructure.Abstract;
using Basket.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Infrastructure
{
    public static class InjectDependencies
    {
        public static void InjectInfrastructure(this IServiceCollection services) 
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
        }
    }
}
