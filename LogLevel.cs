namespace NekoLogger
{
    /// <summary>
    /// Logging Level, defines which levels are logged
    /// Only the current and lower levels are logged
    /// (For example if level 3 is chosen then levels 3 2 1 are gonna be logged)
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Logging is Disabled, no logs, even fatal ones aren't displays
        /// You cannot send a log on this level
        /// </summary>
        DISABLED = 0,

        /// <summary>
        /// Fatal error is an unrecoverable error that leads to the program being unusable
        /// </summary>
        FATAL = 1,

        /// <summary>
        /// The error is recoverable but not fixing it might lead to data loss or corruption or other undesirable behaviour
        /// </summary>
        CRITICAL = 2,

        /// <summary>
        /// The error is recoverable but the program wasn't able to correct it, less severe than CRITICAL
        /// </summary>
        ERROR = 3,

        /// <summary>
        /// The error is recoverable and the program was able to correct
        /// </summary>
        WARN = 4,

        /// <summary>
        /// Not neccessarily an error but a warning that something is out of the ordinary and should potentially be looked into
        /// </summary>
        NOTICE = 5,

        /// <summary>
        /// Messages about ordinary operation of the application
        /// </summary>
        INFO = 6,

        /// <summary>
        /// More verbose messages about the operation of the application, mainly useful for debugging
        /// </summary>
        DEBUG = 7,

        /// <summary>
        /// More fine-grained than debug, it is the most verbose log level
        /// WARNING : Potential Log Spam
        /// </summary>
        TRACE = 8,

        /// <summary>
        /// A shorthand for the TRACE log level, cannot be logged into
        /// </summary>
        ALL = 9
    }
}
