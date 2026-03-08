using Mail.IoC;
using MailManager.Application.IoC;
using MailManager.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .RegisterApplicationServices(builder.Configuration)
    .RegisterInfrastructureServices(builder.Configuration)
    .RegisterWorkers();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});


app.MapControllers();
app.Run();