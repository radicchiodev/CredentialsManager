namespace Mail.IoC;

public static class RegisterWorkerDependencies
{
    public static IServiceCollection RegisterWorkers(this IServiceCollection services)
    {
        services
            .AddHostedService<MailWorker>();
        
        return services;
    }
}