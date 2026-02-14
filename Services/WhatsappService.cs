namespace NotificationGateway.Api.Services;

public class WhatsappService : INotificationService
{
    public async Task SendAsync(NotificationRequest request)
    {
        System.Console.WriteLine(
            $"[WHATSAPP]Enviando mensagem para Whatsapp... | Destinatário: {request.Email}",
            request.Email
        );
        await Task.CompletedTask;
    }
}
