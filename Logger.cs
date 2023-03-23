using System.Data;

namespace NekoLogger
{
    /// <summary>
    /// An instance of a Logger
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Defines the options of the logger
        /// </summary>
        private LoggerOptions options;

        private StreamWriter? logWriter;

        private List<LogLine> logBuffer;


        private static Dictionary<LogLevel, ConsoleColor> colors = new Dictionary<LogLevel, ConsoleColor>()
        {
            {LogLevel.TRACE, ConsoleColor.DarkGray },
            {LogLevel.DEBUG, ConsoleColor.Gray },
            {LogLevel.INFO, ConsoleColor.White },
            {LogLevel.NOTICE, ConsoleColor.Blue },
            {LogLevel.WARN, ConsoleColor.Yellow },
            {LogLevel.ERROR, ConsoleColor.Red },
            {LogLevel.CRITICAL, ConsoleColor.DarkMagenta },
            {LogLevel.FATAL, ConsoleColor.DarkRed }
        };


        /// <summary>
        /// Initializes a Logger Instance, if no logger options are specified, LoggerOptions.DEFAULT is used
        /// </summary>
        public Logger(LoggerOptions? options = null)
        {
            // Initialize Buffer
            logBuffer = new List<LogLine>();

            // Check if LoggerOptions has been defined
            if (options == null)
            {
                // If LoggerOptions has not been defined, use the defaults
                this.options = LoggerOptions.DEFAULT;
            }
            else
            {
                // If LoggerOptions has been defined, check if it is valid
                this.options = new LoggerOptions();

                // If LoggerOptions is defined but is not valid, set the missing parameter to the default Log Level and Log the warning about it
                bool consoleAlert = false;
                bool fileAlert = false;
                bool logPathAlert = false;

                // Check if consoleloglevel is invalid
                if (options.consoleLogLevel == null || options.consoleLogLevel.Value > LogLevel.ALL || options.consoleLogLevel.Value < LogLevel.DISABLED)
                {
                    this.options.consoleLogLevel = LogLevel.INFO;
                    consoleAlert = true;
                }
                else
                {
                    this.options.consoleLogLevel = options.consoleLogLevel;
                }

                Info("Initializing Logger");

                Trace("Parsing the fileLogLevel config");
                // Check if fileloglevel is invaid
                if (options.fileLogLevel == null || options.fileLogLevel.Value > LogLevel.ALL || options.fileLogLevel.Value < LogLevel.DISABLED)
                {
                    this.options.fileLogLevel = LogLevel.INFO;
                    fileAlert = true;
                }
                else
                {
                    this.options.fileLogLevel = options.fileLogLevel;
                }

                Trace("Parsing the log directory");
                // Check if log directory is valid
                if (options.logDirectory == null)
                {
                    this.options.logDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "logs" + Path.DirectorySeparatorChar;
                    logPathAlert = true;
                }
                else
                {
                    this.options.logDirectory = options.logDirectory;
                }

                // Check if the logging directory exists, if not create it
                Debug("Logging directory set to " + this.options.logDirectory);
                if (!Directory.Exists(this.options.logDirectory))
                {
                    Debug("Log Directory didn't exist, creating");
                    Directory.CreateDirectory(this.options.logDirectory);
                }

                // Generate the log filename
                Trace("Generating log filename");
                string datenow = DateTime.Now + "";
                datenow = datenow.Replace(" ", "_").Replace(":", "_").Replace(".", "_");
                string logPath = datenow + ".log";

                // Try to initiate a StreamWriter
                try
                {
                    Debug("Creating new log file : " + logPath);
                    logWriter = new StreamWriter(this.options.logDirectory + @"\" + logPath);
                    logWriter.AutoFlush = true;
                }
                catch (Exception e)
                {
                    Error("There was an error creating the log file, logging to file disabled");
                    Error(e.Message);
                    this.options.fileLogLevel = LogLevel.DISABLED;
                }

                // Now that log levels are valid, issue alerts on the WARN Log Level
                if (consoleAlert)
                {
                    Warn("ConsoleLogLevel value is invalid, using the INFO level");
                }
                if (fileAlert)
                {
                    Warn("FileLogLevel value is invalid, using the INFO level");
                }
                if(logPathAlert)
                {
                    Warn("Logging Directory not specified, using the default logging path");
                }
            }

            // Log the status about the initialization of the Logger
            Debug("Initializing NekoLogger with ConsoleLogLevel " + this.options.consoleLogLevel + " and FileLogLevel " + this.options.fileLogLevel);
            Info("NekoLogger Initialized");

            if(this.options.fileLogLevel <= 0)
            {
                Info("File Logging is DISABLED");
            }

            if(this.options.consoleLogLevel <= 0)
            {
                Info("Console Logging is DISABLED");
            }
        }

        // TRACE //
        /// <summary>
        /// Logs the message at the TRACE Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        public void Trace(string message)
        {
            Log(LogLevel.TRACE, message);
        }

        /// <summary>
        /// Logs the message together with an exception at the TRACE Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        /// <param name="e">Exception to Log</param>
        public void Trace(string message, Exception e)
        {
            Log(LogLevel.TRACE, message + "\n" + e.Message + "\n" + e.StackTrace);
        }

        // DEBUG //
        /// <summary>
        /// Logs the message at the DEBUG Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        public void Debug(string message)
        {
            Log(LogLevel.DEBUG, message);
        }

        /// <summary>
        /// Logs the message together with an exception at the DEBUG Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        /// <param name="e">Exception to Log</param>
        public void Debug(string message, Exception e)
        {
            Log(LogLevel.DEBUG, message + "\n" + e.Message + "\n" + e.StackTrace);
        }

        // INFO //
        /// <summary>
        /// Logs the message at the INFO Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        public void Info(string message)
        {
            Log(LogLevel.INFO, message);
        }

        /// <summary>
        /// Logs the message together with an exception at the INFO Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        /// <param name="e">Exception to Log</param>
        public void Info(string message, Exception e)
        {
            Log(LogLevel.INFO, message + "\n" + e.Message + "\n" + e.StackTrace);
        }

        // NOTICE //
        /// <summary>
        /// Logs the message at the NOTICE Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        public void Notice(string message)
        {
            Log(LogLevel.NOTICE, message);
        }

        /// <summary>
        /// Logs the message together with an exception at the NOTICE Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        /// <param name="e">Exception to Log</param>
        public void Notice(string message, Exception e)
        {
            Log(LogLevel.NOTICE, message + "\n" + e.Message + "\n" + e.StackTrace);
        }

        // WARN //
        /// <summary>
        /// Logs the message at the WARN Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        public void Warn(string message)
        {
            Log(LogLevel.WARN, message);
        }

        /// <summary>
        /// Logs the message together with an exception at the WARN Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        /// <param name="e">Exception to Log</param>
        public void Warn(string message, Exception e)
        {
            Log(LogLevel.WARN, message + "\n" + e.Message + "\n" + e.StackTrace);
        }

        // ERROR //
        /// <summary>
        /// Logs the message at the ERROR Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        public void Error(string message)
        {
            Log(LogLevel.ERROR, message);
        }

        /// <summary>
        /// Logs the message together with an exception at the ERROR Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        /// <param name="e">Exception to Log</param>
        public void Error(string message, Exception e)
        {
            Log(LogLevel.ERROR, message + "\n" + e.Message + "\n" + e.StackTrace);
        }

        // CRITICAL //
        /// <summary>
        /// Logs the message at the CRITICAL Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        public void Critical(string message)
        {
            Log(LogLevel.CRITICAL, message);
        }

        /// <summary>
        /// Logs the message together with an exception at the CRITICAL Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        /// <param name="e">Exception to Log</param>
        public void Critical(string message, Exception e)
        {
            Log(LogLevel.CRITICAL, message + "\n" + e.Message + "\n" + e.StackTrace);
        }

        // FATAL //
        /// <summary>
        /// Logs the message at the FATAL Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        public void Fatal(string message)
        {
            Log(LogLevel.FATAL, message);
        }

        /// <summary>
        /// Logs the message together with an exception at the FATAL Log Level
        /// </summary>
        /// <param name="message">Message to Log</param>
        /// <param name="e">Exception to Log</param>
        public void Fatal(string message, Exception e)
        {
            Log(LogLevel.FATAL, message + "\n" + e.Message + "\n" + e.StackTrace);
        }

        /// <summary>
        /// Logs the message at the specified log level
        /// </summary>
        /// <param name="logLevel">Level to Log at</param>
        /// <param name="message">Message to Log</param>
        public void Log(LogLevel logLevel, string message)
        {
            if(logLevel <= LogLevel.DISABLED || logLevel >= LogLevel.ALL)
            {
                return;
            }

            if (logBuffer.Count > 0 && logWriter != null)
            {
                foreach (var item in logBuffer)
                {
                    _log(item.LogLevel, item.Message+"", true);
                }
                logBuffer.Clear();
            }

            _log(logLevel, message);
        }

        // Create The Values that are gonna be used when constructing logging messages to prevent their reinitialization
        private ConsoleColor previousColor;
        private DateTime timeNow;
        private string? logLine;

        /// <summary>
        /// Private Method which gets called by all the other log methods
        /// </summary>
        private void _log(LogLevel logLevel, string message, bool onlyFile = false)
        {
            timeNow = DateTime.Now;
            logLine = "[" + timeNow.Hour.ToString().PadLeft(2,'0') + ":" + timeNow.Minute.ToString().PadLeft(2, '0') + ":" + timeNow.Second.ToString().PadLeft(2, '0') + "] [" + logLevel.ToString() + "] " + message;

            // Log to File
            if(logLevel <= options.fileLogLevel || logWriter == null)
            {
                if(logWriter != null)
                {
                    logWriter.WriteLine(logLine);
                }
                else
                {
                    logBuffer.Add(new LogLine() { LogLevel = logLevel, Message = message });
                }
            }

            // Log to Console
            if (logLevel <= options.consoleLogLevel && !onlyFile)
            {
                // Store the Previous Color
                previousColor = Console.ForegroundColor;

                Console.ForegroundColor = colors[logLevel];
                Console.WriteLine(logLine);

                // Restore the previous Console Color
                Console.ForegroundColor = previousColor;
            }

        }
    }
}
