using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Sistema de Notificaciones - Ejecución de pruebas ===\n");

        // Casos correctos
        var emailValido = new EmailNotification("usuario@ejemplo.com", "Asunto", "Cuerpo del mensaje");
        var smsValido = new SmsNotification("+34123456789", "Mensaje SMS de prueba");
        var pushValido = new PushNotification("device-token-123", "Notificación push de prueba");

        EjecutarEnvio(emailValido);
        EjecutarEnvio(smsValido);
        EjecutarEnvio(pushValido);

        Console.WriteLine();

        // Casos incorrectos
        var emailInvalido = new EmailNotification("usuario-ejemplo.com", "Asunto", "Cuerpo"); // falta @
        var smsInvalido = new SmsNotification("", "Mensaje"); // número vacío
        var pushInvalido = new PushNotification(null, "Payload"); // token nulo

        EjecutarEnvio(emailInvalido);
        EjecutarEnvio(smsInvalido);
        EjecutarEnvio(pushInvalido);

        Console.WriteLine("\n=== Pruebas completadas ===");
    }

    static void EjecutarEnvio(INotification notificacion)
    {
        Console.WriteLine($"Enviando {notificacion.GetType().Name}...");
        try
        {
            notificacion.Send();
            Console.WriteLine("Resultado: Envío exitoso.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Resultado: Error en el envío -> {ex.Message}\n");
        }
    }
}

public interface INotification
{
    void Send();
}

public class EmailNotification : INotification
{
    public string To { get; }
    public string Subject { get; }
    public string Body { get; }

    public EmailNotification(string to, string subject, string body)
    {
        To = to;
        Subject = subject;
        Body = body;
    }

    public void Send()
    {
        if (string.IsNullOrWhiteSpace(To) || !To.Contains("@"))
            throw new InvalidOperationException("Dirección de email inválida.");

        // Simular envío
        Console.WriteLine($"Email enviado a: {To} | Asunto: {Subject}");
    }
}

public class SmsNotification : INotification
{
    public string PhoneNumber { get; }
    public string Message { get; }

    public SmsNotification(string phoneNumber, string message)
    {
        PhoneNumber = phoneNumber;
        Message = message;
    }

    public void Send()
    {
        if (string.IsNullOrWhiteSpace(PhoneNumber))
            throw new InvalidOperationException("Número de teléfono inválido.");

        // Simular envío
        Console.WriteLine($"SMS enviado a: {PhoneNumber} | Mensaje: {Message}");
    }
}

public class PushNotification : INotification
{
    public string DeviceToken { get; }
    public string Payload { get; }

    public PushNotification(string deviceToken, string payload)
    {
        DeviceToken = deviceToken;
        Payload = payload;
    }

    public void Send()
    {
        if (string.IsNullOrWhiteSpace(DeviceToken))
            throw new InvalidOperationException("Token de dispositivo inválido.");

        // Simular envío
        Console.WriteLine($"Push enviado a token: {DeviceToken} | Payload: {Payload}");
    }
}
