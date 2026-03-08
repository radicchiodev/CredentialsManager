using System.Text.Json.Serialization;

namespace MailManager.Infrastructure.Entities;

public class CreateMailRuleEntity
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; private set; }

    [JsonPropertyName("name")]
    public string Name { get; private set; } = null!;

    [JsonPropertyName("actions")]
    public List<Action> Actions { get; private set; } = null!;

    [JsonPropertyName("matchers")]
    public List<Matcher> Matchers { get; private set; } = null!;

    public static CreateMailRuleEntity CreateRule(string mailForwardTo, string mailFrom)
    {
        return new CreateMailRuleEntity
        {
            Enabled = true,
            Name = $"Rule created at {DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}",
            Actions = new List<Action>{ new Action
            {
                Type = "forward",
                Value = new List<string>{ mailForwardTo }
            }},
            Matchers = new List<Matcher>
            {
                new Matcher
                {
                    Type = "literal",
                    Field = "to",
                    Value = mailFrom
                }
            }
        };
    }
}
public class Action
{
    [JsonPropertyName("type")] 
    public string Type { get; set; } = null!;

    [JsonPropertyName("value")] 
    public List<string> Value { get; set; } = null!;
}

public class Matcher
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("field")]
    public string Field { get; set; } = null!;

    [JsonPropertyName("value")]
    public string Value { get; set; } = null!;
}