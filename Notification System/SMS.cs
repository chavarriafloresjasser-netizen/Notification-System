class SMS : BaseNotification
{
    public SMS(string recipient, string message) : base(recipient, message)
    {
    }

    protected override string MessageDisplay()
    {
        return $"SMS para {ValidateRecipient()}: " + $"\n{ValidateMessage()}";
    }

    protected override string ValidateRecipient()
    {
        if (string.IsNullOrWhiteSpace(Recipient))
            throw new ArgumentException("ERROR: El destinatario no puede quedar vacío.");
        if (!long.TryParse(Recipient, out _))
            throw new ArgumentException("ERROR: El destinatario debe ser un número de teléfono válido.");
        if (Recipient.Length != 8)
            throw new ArgumentException("ERROR: El destinatario debe tener 8 dígitos.");

        return Recipient;
    }

    protected override string ValidateMessage()
    {
        int caracteresMaximos = 160; // Establece el límite de caracteres para un mensaje SMS
        if (Message.Length > caracteresMaximos)
            throw new ArgumentException($"ERROR: El mensaje no puede exceder {caracteresMaximos} caracteres.");
        return Message;
    }

    protected override string DisplayInformation()
    {
        return base.DisplayInformation() + $" || Tipo de notificación: SMS";
    }

}