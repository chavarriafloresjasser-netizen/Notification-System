abstract class BaseNotification
{
    private string? _message;
    private string? _recipient;
    private bool _status;
    private int _counterMenssage;
    private DateTime _lastSend;
    private DateTime _sendingTime;
    private readonly TimeSpan TimeBetweenSends = TimeSpan.FromMinutes(1); // Tiempo mínimo entre envíos

    //Constructor
    /// <summary>
    /// Constructor de la clase BaseNotification, que inicializa el destinatario, el mensaje, el estado de la notificación, el contador de mensajes enviados y la fecha del último envío. El estado se establece inicialmente como "no enviado", el contador de mensajes se inicia en cero y la fecha del último envío se establece en una fecha mínima para indicar que no se ha enviado ningún mensaje aún.
    /// </summary>
    /// <param name="recipient">El destinatario de la notificación</param>
    /// <param name="message">El mensaje de la notificación</param>
    public BaseNotification(string recipient, string message)
    {
        Recipient = recipient;
        Message = message;
        Status = false;
        CounterMenssage = 0;
        LastSend = DateTime.MinValue; // Inicializa con una fecha mínima para indicar que no se ha enviado ningún mensaje aún
    }

    //Protected methods
    /// <summary>
    /// Metodo creado para que todas las clases hijas puedan validar el tipo de destinatario, ya que todos los tipos de notificaciones contiene diferentes formas para llamar al usuariio, ya sea por correo o por numero
    /// </summary>
    /// <returns>Validación del usuario</returns>
    protected abstract string ValidateRecipient();
    /// <summary>
    /// Metodo creado para que las clases hijas puedan implementarsu propia forma de mostrar el mensaje, ya que cada tipo de notificación puede tener un formato diferente.
    /// </summary>
    /// <returns>Formato del mensaje</returns>
    protected abstract string MessageDisplay();
    /// <summary>
    /// Metodo hecho para validar los tipos de mensajes, ya que cada tipo de notificación puede tener diferentes reglas para validar el mensaje, como por ejemplo, un correo electrónico puede requerir un formato específico, mientras que un mensaje de texto puede tener restricciones de longitud.
    /// </summary>
    /// <returns>Validación del mensaje</returns>
    protected abstract string ValidateMessage();
    //Virtual methods
    /// <summary>
    /// Despliega información relevante sobre la notificación, como el destinatario, la cantidad de mensajes enviados y la fecha del último mensaje enviado. Este método puede ser sobrescrito por las clases hijas para mostrar información adicional o diferente según el tipo de notificación.
    /// </summary>
    /// <returns>Información de la notificación</returns>
    virtual protected string DisplayInformation()
    {
        return $"Destinatario: {ValidateRecipient} || Cantidad de mensajes enviados {CounterMenssage}|| Ultimo mensaje enviado {LastSend}";
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
        if (string.IsNullOrWhiteSpace(Message) || string.IsNullOrWhiteSpace(ValidateRecipient()))
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
            CounterMenssage++;
            LastSend = DateTime.Now;
            return $"Notificación enviada a {ValidateRecipient}";
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
            _message = value.Trim();
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