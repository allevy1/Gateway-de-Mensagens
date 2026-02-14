namespace NotificationGateway.Api.Services;

public interface INotificationService
{
    Task SendAsync(NotificationRequest request);
}
