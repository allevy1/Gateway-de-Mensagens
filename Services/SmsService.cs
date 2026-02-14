namespace NotificationGateway.Api.Services;

public class SmsService : INotificationService
{
    public async Task SendAsync(NotificationRequest request)
    {
        System.Console.WriteLine(
            $"[SMS]Enviando mensagem para SMS... | Destinatário: {request.Email}",
            request.Email
        );
        await Task.CompletedTask;
    }
}
