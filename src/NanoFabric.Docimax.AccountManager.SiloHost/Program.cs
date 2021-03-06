﻿using Microsoft.Extensions.Logging;
using NanoFabric.Docimax.Grains.AccountManager;
using Orleans;
using Orleans.Authentication;
using Orleans.Authentication.IdentityServer4;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Hosting.Development;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NanoFabric.Docimax.AccountManager.SiloHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var builder = new SiloHostBuilder()               
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "AccountTransferApp";
                })
                .UseConsulClustering(options => {
                    options.Address = new Uri("http://127.0.0.1:8500");
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(AccountGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole())
                .AddMemoryGrainStorageAsDefault()
                .UseInClusterTransactionManager()
                .UseInMemoryTransactionLog()
                .UseTransactionalState()
                .AddAuthentication((HostBuilderContext context, AuthenticationBuilder authen) =>
                {
                    authen.AddIdentityServerAuthentication(opt =>
                    {
                        opt.RequireHttpsMetadata = true;
                        opt.Authority = "http://localhost:50774";
                        opt.ApiName = "DocimaxHeros";
                    });
                }, IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddAuthorizationFilter();

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
