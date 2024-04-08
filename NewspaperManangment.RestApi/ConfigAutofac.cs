﻿using Autofac.Extensions.DependencyInjection;
using Autofac;

namespace NewspaperManangment.RestApi
{
    public static class ConfigAutofac
    {
        public static ConfigureHostBuilder AddAutofac(
           this ConfigureHostBuilder builder,
           IConfiguration configuration)
        {
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.ConfigureContainer<ContainerBuilder>(b =>
               b.RegisterModule(new AutofacBusinessModule(configuration))
            );
            return builder;
        }
    }
  
}
