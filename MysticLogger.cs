using System;

public class MysticLogger
{
	private delegate void CustomLogger(string log);
    private delegate void CustomWarningLogger(string log);
	private delegate void CustomExceptionLogger(Exception exception);

	static CustomLogger logger;
	static CustomWarningLogger wLogger;
	static CustomExceptionLogger eLogger;

    /** <summary>   
         Custom all-purpose logger.
     <param name="_logger"/> A function which takes a string as a parameter.
     <example>
         <para> Example:  <c>MysticLogger.SetLoggingFunction((string text) => Console.WriteLine(text));</c> </para>
     </example>
     </summary> */
	public static void SetLoggingFunction(CustomLogger _logger)
	{
		logger = _logger;
	}
    
    /** <summary>   
         Custom logger intended for logging warnings.
     <param name="_logger"/> Any function which takes a string as a parameter.
     <example>
         <c>MysticLogger.SetLoggingFunction((string text) => Console.WriteLine(text));</c>
     </example>
     </summary> */
	public static void SetWarningLoggingFunction(CustomWarningLogger _logger)
	{
		wLogger = _logger;
	}
	
    /** <summary>   
         Custom exception logger. Hook up to a string output function to receive logs from MysticNetworking.
     <param name="_logger"/> Any function which takes a string as a parameter.
     <example>
         <c>MysticLogger.SetLoggingFunction((Execption e) => Console.WriteLine(e));</c>
     </example>
     </summary> */
	public static void SetExceptionLoggingFunction(CustomExceptionLogger _logger)
	{
		eLogger = _logger;
	}

    /** <summary>   
         Set this function for all loggers.
     <param name="_logger"/> Any function which takes a string as a parameter.
     <example>
         <c>MysticLogger.SetAllLoggingFunctions((string text) => Console.WriteLine(text));</c>
     </example>
     </summary> */
    public static void SetAllLoggingFunctions(CustomLogger _logger)
    {
        logger = _logger;
        wLogger = _logger;
        eLogger = _logger;
    }

    // TODO: rework so there is one log function which uses params to indicate the log type.
	public static void Log(object message)
	{
        try {
            string messageString = object.ToString();
        }
        catch(Exception ex)
        {
            throw new Exception("Unable to convert to string.", ex);
        }

        try {
		    logger(messageString);
        }
        catch(Exception ex)
        {
            throw new NullReferenceException("No function has been set for MysticLogger logger", ex);
        }
	}

    // TODO: Fallback
	public static void LogWarning(object message)
	{
        try {
            string messageString = object.ToString();
        }
        catch(Exception ex)
        {
            throw new Exception("Unable to convert to string.", ex);
        }

		try {
		    wLogger(messageString);
        }
        catch(Exception ex)
        {
            throw new NullReferenceException("No function has been set for MysticLogger logger", ex);
        }
	}

	public static void LogException(Exception exception)
	{
        // if the exception logger is not set then fallback to the other loggers.
        // unfortunatly we cannot log this so we gotta throw an exeption. Perhaps this is dumb idk

        try{
            // display as an exeption if possible
            if (eLogger == CustomExceptionLogger)
                eLogger(exception);
            // fallbacks
            else if (wLogger == CustomWarningLogger)
                wLogger(exception);
            else
                logger(exception);
        }
        catch(Exception ex)
        {
            throw new NullReferenceException("No logging function has been set for MysticLogger", ex);
        }
	}

}
