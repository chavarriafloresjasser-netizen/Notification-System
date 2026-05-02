class GMAIL : BaseNotification
{
    public GMAIL(string recipient, string message) : base(recipient, message)
    {
    }

    protected override string MessageDisplay()
    {
        return $"GMAIL para {ValidateRecipient()}: " + $"\n{ValidateMessage()}";
    }

    protected override string ValidateRecipient()
    {
        if (string.IsNullOrWhiteSpace(Recipient))
            throw new ArgumentException("ERROR: El destinatario no puede quedar vacío.");
        if (!Recipient.Contains("@") || !Recipient.Contains("."))
            throw new ArgumentException("ERROR: El destinatario debe ser un correo electrónico válido.");
        return Recipient;
    }

    protected override string ValidateMessage()
    {
        if (Message.Length > 1000)
            throw new ArgumentException("ERROR: El mensaje no puede exceder 1000 caracteres.");
        return Message;
    }
}