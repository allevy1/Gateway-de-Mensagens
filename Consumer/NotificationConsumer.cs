namespace NotificationGateway.Api.Consumer;

public class NotificationRequestConsumer : IConsumer<NotificationRequest>
{
    private readonly ILogger<NotificationRequestConsumer> _logger;
    private readonly WhatsappService _whatsappService;
    private readonly SmsService _smsService;
    private readonly EmailService _emailService;

    public NotificationRequestConsumer(
        ILogger<NotificationRequestConsumer> logger,
        WhatsappService whatsappService,
        SmsService smsService,
        EmailService emailService
    )
    {
        _logger = logger;
        _emailService = emailService;
        _smsService = smsService;
        _whatsappService = whatsappService;
    }

    public async Task Consume(ConsumeContext<NotificationRequest> context)
    {
        var dados = context.Message;

        _logger.LogInformation(
            "Processando notificação. Canal: {Canal} | Destinatário: {Email}",
            dados.Canal,
            dados.Email
        );

        try
        {
            switch (dados.Canal.ToLower())
            {
                case "email":
                    await _emailService.SendAsync(context.Message);
                    break;
                case "whatsapp":
                    await _whatsappService.SendAsync(context.Message);
                    break;
                case "sms":
                    await _smsService.SendAsync(context.Message);
                    break;
                default:
                    // Log de Aviso (Amarelo)
                    _logger.LogWarning("Canal não suportado: {Canal}", dados.Canal);
                    break;
            }
        }
        catch (Exception ex)
        {
            // Log de Erro (Vermelho) - O MassTransit vai capturar e iniciar o Retry
            _logger.LogError(ex, "Erro ao processar notificação para {Email}", dados.Email);
            throw;
        }

        await Task.CompletedTask;
    }
}
