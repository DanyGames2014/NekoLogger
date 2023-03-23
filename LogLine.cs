﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoLogger
{
    /// <summary>
    /// Class representing a line in a log, used for buffering
    /// </summary>
    public class LogLine
    {
        /// <summary>
        /// Level of the log
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Message of the log
        /// </summary>
        public string? Message { get; set; }
    }
}
