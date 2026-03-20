using MassTransit;
using NotificationGateway.Contracts;
using NotificationGateway.Worker.Consumer;
using NotificationGateway.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<SmsService>();
builder.Services.AddScoped<WhatsappService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NotificationRequestConsumer>();

    x.UsingRabbitMq(
        (context, cfg) =>
        {
            cfg.Host("localhost", "/");

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
