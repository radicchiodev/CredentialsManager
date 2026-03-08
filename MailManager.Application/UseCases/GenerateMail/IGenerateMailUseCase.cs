namespace MailManager.Application.UseCases.GenerateMail;

public interface IGenerateMailUseCase
{
    Task ExecuteAsync(string hash);
}