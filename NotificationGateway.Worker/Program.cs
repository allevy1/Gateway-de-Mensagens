using MassTransit;
using NotificationGateway.Contracts;
using NotificationGateway.Worker.Consumer;
using NotificationGateway.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<SmsService>();
builder.Services.AddScoped<WhatsappService>();

var rabbitMqSettings = builder.Configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NotificationRequestConsumer>();

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

            cfg.UseMessageRetry(r =>
                r.Exponential(
                    5,
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(30),
                    TimeSpan.FromSeconds(5)
                )
            );

            cfg.ReceiveEndpoint(
                "notification-queue",
                e =>
                {
                    e.ConfigureConsumer<NotificationRequestConsumer>(context);
                }
            );
        }
    );
});

var host = builder.Build();
host.Run();
