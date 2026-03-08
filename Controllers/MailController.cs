using MailManager.Application.Config;
using MailManager.Application.UseCases.GenerateMail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Mail.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MailController : ControllerBase
{
    private readonly ILogger<MailController> _logger;
    private readonly IGenerateMailUseCase _generateMailUseCase;
    private readonly MailConfiguration _mailConfiguration;
    
    public MailController(ILogger<MailController> logger, 
        IGenerateMailUseCase generateMailUseCase, 
        IOptions<MailConfiguration> mailConfiguration)
    {
        _mailConfiguration = mailConfiguration.Value;
        _logger = logger;
        _generateMailUseCase = generateMailUseCase;
    }

    [HttpGet("GetDomain")]
    public IActionResult GetDomain()
    {
        return Ok(new { Domain = _mailConfiguration.Domain });
    }
    [HttpPost()]
    public async Task<IResult> CreateMail([FromBody]GenerateMailDTO mail)
    {
        await _generateMailUseCase.ExecuteAsync(mail.Name);
        return Results.Ok();
    }
}