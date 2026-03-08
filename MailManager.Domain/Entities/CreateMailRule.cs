namespace MailManager.Domain.Entities;

public class CreateMailRule
{
    public string EmailForwardTo { get; set; } = null!;
    public string EmailFrom { get; set; } = null!;
}