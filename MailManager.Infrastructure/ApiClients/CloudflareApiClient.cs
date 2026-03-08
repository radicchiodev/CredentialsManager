using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MailManager.Application.Interfaces;
using MailManager.Domain.Entities;
using MailManager.Infrastructure.Config;
using MailManager.Infrastructure.Mappers;
using Microsoft.Extensions.Options;

namespace MailManager.Infrastructure.ApiClients;

public class CloudflareApiClient : ICloudflareApiClient
{
    private readonly CloudfareSettings _cloudfareSettings;
    
    public CloudflareApiClient(IOptions<CloudfareSettings> cloudfareSettings)
    {
        _cloudfareSettings = cloudfareSettings.Value;
    }

    public async Task PostRuleAsync(CreateMailRule domainRule)
    {
        var httpClient = new HttpClient();
        string ApiBase = "https://api.cloudflare.com/client/v4";
        var rule = CreateMailRuleMapper.ToEntity(domainRule);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cloudfareSettings.APIToken);

        string json = JsonSerializer.Serialize(rule);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync($"{ApiBase}/zones/{_cloudfareSettings.ZoneId}/email/routing/rules", content);
        
        response.EnsureSuccessStatusCode();
    }
}