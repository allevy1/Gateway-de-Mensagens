using MassTransit;

Environment.SetEnvironmentVariable("MT_LICENSE", "community");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq(
        (context, cfg) =>
        {
            cfg.Host("localhost");
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
