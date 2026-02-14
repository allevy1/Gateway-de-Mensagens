var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    // 1. Registra o seu Consumer (NotificationRequestConsumer)
    x.AddConsumer<NotificationRequestConsumer>();

    x.UsingRabbitMq(
        (context, cfg) =>
        {
            // 2. Endereço do seu Docker (localhost é o padrão)
            cfg.Host("localhost");

            cfg.UseMessageRetry(r =>
            {
                r.Interval(5, TimeSpan.FromSeconds(3));
            });

            // 3. Isso cria a fila automaticamente no RabbitMQ
            cfg.ConfigureEndpoints(context);
        }
    );
});

builder.Services.AddControllers();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<SmsService>();
builder.Services.AddScoped<WhatsappService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
