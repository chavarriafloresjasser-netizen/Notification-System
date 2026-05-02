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
    protected abstract string MessageDisplay();
    //Virtual methods
    virtual protected string DisplayInformation()
    {
        return $"Destinatario: {Recipient} || Cantidad de mensajes enviados {CounterMenssage}|| Ultimo mensaje enviado {LastSend}";
    }
    //Public methods
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