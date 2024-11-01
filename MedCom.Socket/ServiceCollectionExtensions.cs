using MedCom.Socket.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MedCom.Socket
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHL7(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MedComSetting>(configuration.GetSection("MedComSetting"));
            //services.AddSingleton<IProtocal, DefaultAppSettings>();
            return services;
        }
    }
}
