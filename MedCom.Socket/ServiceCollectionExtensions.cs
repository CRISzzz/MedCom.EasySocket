using MedCom.EasySocket.Core;
using MedCom.EasySocket.HL7;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MedCom.EasySocket
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHL7(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MedComOption>(configuration.GetSection("MedComOption"));
            services.AddSingleton<IProtohl7, Protohl7>();
            return services;
        }

        public static IServiceCollection AddDICOM(this IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}
