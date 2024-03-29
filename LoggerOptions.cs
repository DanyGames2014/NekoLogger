﻿namespace NekoLogger
{
    /// <summary>
    /// Represents options to create a Logger with
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// Log level of the file, independent from the console log level to prevent unnecessary writes with high log level
        /// </summary>
        public LogLevel? fileLogLevel;

        /// <summary>
        /// Log level of the console
        /// </summary>
        public LogLevel? consoleLogLevel;

        /// <summary>
        /// Path to the directory to write logs into
        /// </summary>
        public string? logDirectory;

        /// <summary>
        /// Limits the amount of messages that can be stored in a buffer
        /// </summary>
        public int bufferLimit;

        /// <summary>
        /// Initializes a new instnace of LoggerOptions, use object initializer to initialize
        /// </summary>
        public LoggerOptions() {}

        /// <summary>
        /// Default settings where both of the logger options are true
        /// </summary>
        public static LoggerOptions DEFAULT = new LoggerOptions()
        {
            consoleLogLevel = LogLevel.INFO,
            fileLogLevel = LogLevel.INFO,
            logDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "logs" + Path.DirectorySeparatorChar,
            bufferLimit = 50
        };
    }
}
