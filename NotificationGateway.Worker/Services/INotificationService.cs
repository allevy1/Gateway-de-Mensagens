namespace NotificationGateway.Worker.Services;

public interface INotificationService
{
    Task SendAsync(NotificationRequest request);
}
