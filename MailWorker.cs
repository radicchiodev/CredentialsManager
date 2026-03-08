using MailManager.Application.UseCases.GenerateMail;

namespace Mail;

public class MailWorker : BackgroundService
{
    private readonly IGenerateMailUseCase _generateMailUseCase;
    private readonly ILogger<MailWorker> _logger;
    public MailWorker(IGenerateMailUseCase generateMailUseCase, ILogger<MailWorker> logger)
    {
        _logger = logger;
        _generateMailUseCase = generateMailUseCase;
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        do
        {
            //await _generateMailUseCase.ExecuteAsync();
            await Task.Delay(1000, cancellationToken);
        } while (!cancellationToken.IsCancellationRequested);
    }
}