using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SozlukProject.Infrastructure.BCryptNet;
using SozlukProject.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void ImplementInfrastructureServices(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            // Automapper
            services.AddAutoMapper(assembly);

            // BCryptNet
            services.AddScoped<IBCryptNet, BCryptHashing>();

            // Flueant Validation
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(assembly))
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
        }
    }
}
