using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var rabbitMqSettings = builder.Configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq(
        (context, cfg) =>
        {
            cfg.Host(
                rabbitMqSettings.Host,
                "/",
                h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                }
            );
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
