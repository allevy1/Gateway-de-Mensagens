namespace NotificationGateway.Api.Requests
{
    public record NotificationRequest
    {
        // [Required] [EmailAddress] string email,
        // [Required] [MaxLength(50000), MinLength(10)] string mensagem,
        // [Required] string canal

        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        [MaxLength(50000), MinLength(10)]
        public string Mensagem { get; init; } = string.Empty;

        [Required]
        public string Canal { get; init; } = string.Empty;

        public NotificationRequest() { }

        public NotificationRequest(string email, string mensagem, string canal)
        {
            Email = email;
            Mensagem = mensagem;
            Canal = canal;
        }
    };
}
