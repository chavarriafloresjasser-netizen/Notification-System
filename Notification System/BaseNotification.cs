abstract class BaseNotification
{
    private string? _message;
    private string? _recipient;
    private bool _status;
    private int _counterMenssage;
    private DateTime _lastSend;
    private DateTime _sendingTime;
    private readonly TimeSpan TimeBetweenSends = TimeSpan.FromMinutes(1); // Tiempo mínimo entre envíos

    //Protected methods
    /// <summary>
    /// Metodo creado para que las clases hijas puedan implementarsu propia forma de mostrar el mensaje, ya que cada tipo de notificación puede tener un formato diferente.
    /// </summary>
    /// <returns>Formato del mensaje</returns>
    protected abstract string MessageDisplay();
    //Virtual methods
    /// <summary>
    /// Despliega información relevante sobre la notificación, como el destinatario, la cantidad de mensajes enviados y la fecha del último mensaje enviado. Este método puede ser sobrescrito por las clases hijas para mostrar información adicional o diferente según el tipo de notificación.
    /// </summary>
    /// <returns>Información de la notificación</returns>
    virtual protected string DisplayInformation()
    {
        return $"Destinatario: {Recipient} || Cantidad de mensajes enviados {CounterMenssage}|| Ultimo mensaje enviado {LastSend}";
    }
    //Public methods
    /// <summary>
    /// Valida que el mensaje y el destinatario no estén vacíos, que la fecha de envío no sea en el pasado y que se haya respetado el tiempo mínimo entre envíos. Si alguna de estas condiciones no se cumple, se lanzará una excepción con un mensaje de error específico. Si todas las validaciones son exitosas, el método devolverá true, indicando que la notificación es válida para ser enviada.
    /// </summary>
    /// <returns>True si la notificación es válida para ser enviada</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public bool Validate()
    {
        if (string.IsNullOrWhiteSpace(Message) || string.IsNullOrWhiteSpace(Recipient))
            throw new ArgumentNullException ("ERROR: El mensaje y/o el destinatario no pueden quedar vacíos.");
        
        if(SendingTime < DateTime.Now)
            throw new ArgumentException("ERROR: La fecha de envío no puede ser en el pasado.");

        if(DateTime.Now - LastSend < TimeBetweenSends)
            throw new InvalidOperationException("ERROR: No se puede enviar la notificación aún. Por favor, espere un momento antes de intentar nuevamente.");
        
        return true;
    }

    /// <summary>
    /// Envia la notificacion si las ultimas validaciones fueron exitosas, actualiza el estado de la notificación a "enviada", registra la fecha y hora del envío, y devuelve un mensaje indicando que la notificación ha sido enviada al destinatario. Si las validaciones no son exitosas, devuelve un mensaje indicando que no se pudo enviar la notificación.
    /// </summary>
    /// <returns>Mensaje indicando el resultado del envío de la notificación</returns>
    public string SendNotification()
    {
        if (Validate())
        {
            Status = true;
            LastSend = DateTime.Now;
            return $"Notificación enviada a {Recipient}";
        }
        return "No se pudo enviar la notificación.";
    }
    //Encapsulations
    public string? Message
    {
        get { return _message; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ERROR: El mensaje no puede quedar vacío.");
            _message = value;
        }
    }

    public string? Recipient
    {
        get { return _recipient; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ERROR: El destinatario no puede quedar vacío.");
            _recipient = value.Trim().ToUpper();
        }
    }

    public bool Status
    {
        get { return _status; }
        private set { _status = value; }
    }
    
    public DateTime SendingTime
    {
        get { return _sendingTime; }
        private set
        {
            if (value < DateTime.Now)
                throw new ArgumentException("ERROR: La fecha de envío no puede ser en el pasado.");
            _sendingTime = value;
        }
    }

    public DateTime LastSend
    {
        get { return _lastSend; }
        private set { _lastSend = value; }
    }

    public int CounterMenssage
    {
        get { return _counterMenssage; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("ERROR: El contador de mensajes no puede ser negativo.");
            _counterMenssage = value;
        }
    }
}