using MailManager.Application.Config;
using MailManager.Application.Interfaces;
using MailManager.Domain.Entities;
using Microsoft.Extensions.Options;

namespace MailManager.Application.UseCases.GenerateMail;

public class GenerateMailUseCase : IGenerateMailUseCase
{
    private readonly ICloudflareApiClient _cloudflareApiClient;
    private readonly MailConfiguration _mailConfiguration;
    public GenerateMailUseCase(ICloudflareApiClient cloudflareApiClient, IOptions<MailConfiguration> configuration)
    {
        _mailConfiguration = configuration.Value;
        _cloudflareApiClient = cloudflareApiClient;
    }
    
    public async Task ExecuteAsync(string name)
    {
        var mail = GenerateMail(name);
        await GenerateMailAddress(mail);
    }

    private string GenerateMail(string name)
    {
        return $"{name}@{_mailConfiguration.Domain}";
    }
    
    private async Task GenerateMailAddress(string mail)
    {
        await _cloudflareApiClient.PostRuleAsync(new CreateMailRule
        {
            EmailForwardTo = _mailConfiguration.ForwardMail,
            EmailFrom = mail
        });
    }
}