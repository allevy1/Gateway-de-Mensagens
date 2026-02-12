namespace NotificationGateway.Api.Consumer;

public class NotificationRequestConsumer : IConsumer<NotificationRequest>
{
    public async Task Consume(ConsumeContext<NotificationRequest> context)
    {
        var dados = context.Message;

        await Task.CompletedTask;
    }
}
