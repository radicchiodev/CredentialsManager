namespace MailManager.Infrastructure.Config;

public class CloudfareSettings
{
    public const string SectionName = "Cloudfare";
    // ReSharper disable once InconsistentNaming
    public string APIToken { get; set; } = null!;
    public string ZoneId { get; set; } = null!;
}