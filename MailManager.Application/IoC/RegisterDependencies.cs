using MailManager.Application.Config;
using MailManager.Application.UseCases.GenerateMail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailManager.Application.IoC;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IGenerateMailUseCase, GenerateMailUseCase>();
        
        services.Configure<MailConfiguration>(
            configuration.GetSection(MailConfiguration.SectionName));
        return services;
    }
}