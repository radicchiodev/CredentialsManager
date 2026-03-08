namespace MailManager.Application.Config;

public class MailConfiguration
{
    public const string SectionName = "Mail";
    public string ForwardMail { get; set; } = null!;
    public string Domain { get; set; } = null!;
}