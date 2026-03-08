using MailManager.Domain.Entities;
using MailManager.Infrastructure.Entities;

namespace MailManager.Infrastructure.Mappers;

public class CreateMailRuleMapper
{
    public static CreateMailRuleEntity ToEntity(CreateMailRule rule)
    {
        return CreateMailRuleEntity.CreateRule(rule.EmailForwardTo, rule.EmailFrom);
    }
}