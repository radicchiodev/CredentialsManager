using MailManager.Application.Interfaces;
using MailManager.Infrastructure.ApiClients;
using MailManager.Infrastructure.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailManager.Infrastructure.IoC;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration  configuration)
    {
        services
            .AddScoped<ICloudflareApiClient, CloudflareApiClient>();
        
        services.Configure<CloudfareSettings>(
            configuration.GetSection(CloudfareSettings.SectionName));
        return services;
    }
}