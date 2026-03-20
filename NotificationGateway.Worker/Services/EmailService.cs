namespace NotificationGateway.Worker.Services;

public class EmailService : INotificationService
{
    public async Task SendAsync(NotificationRequest request)
    {
        System.Console.WriteLine(
            $"[EMAIL]Enviando mensagem para E-mail...| Destinatário: {request.Email}",
            request.Email
        );
        await Task.CompletedTask;
    }
}
