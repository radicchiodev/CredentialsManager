using MailManager.Domain.Entities;

namespace MailManager.Application.Interfaces;

public interface ICloudflareApiClient
{
    Task PostRuleAsync(CreateMailRule rule);
}