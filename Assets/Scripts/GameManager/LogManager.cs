using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public struct Log
    {
        public enum LogType
        {
            Error,
            Important,
            Default,
            Hint,
            Input,
            Automatic
        }

        public enum LogOrigin
        {
            General,
            Experience
        }

        /// <summary>
        /// The content of the log.
        /// </summary>
        public string message;
        /// <summary>
        /// The time of the log.
        /// </summary>
        public float time;
        /// <summary>
        /// The color of the log.
        /// </summary>
        public Color color;
        /// <summary>
        /// The source of the log
        /// </summary>
        public ISource source;
        /// <summary>
        /// The log type.
        /// </summary>
        public LogType logType;
        /// <summary>
        /// The log origin.
        /// </summary>
        public LogOrigin logOrigin;

        public Log(string message, float time, Color color, ISource source, LogType logType, LogOrigin logOrigin)
        {
            this.message = message;
            this.time = time;
            this.color = color;
            this.source = source;
            this.logType = logType;
            this.logOrigin = logOrigin;
        }

        private string TimeFormat(float time)
        {
            return string.Format("[{0}:{1:00}]", (int)(time / 60), (int)(time % 60));
        }

        private string SourceNameFormat(ISource source)
        {
            if (source == null)
                return "[Unknown]";
            return string.Format("[{0}]", source.sourceName.ToUpper());
        }

        public string ToString(bool displayTime)
        {
            if (displayTime)
                return this.ToString();
            return string.Format("<color=#{0}>{1} - {2}</color>", ColorUtility.ToHtmlStringRGB(color), SourceNameFormat(source), message);
        }

        public override string ToString()
        { 
            return string.Format("<color=#{0}>{1}{2} - {3}</color>", ColorUtility.ToHtmlStringRGB(color), TimeFormat(time), SourceNameFormat(source), message);
        }
    }

    public class LogManager
    {
        public delegate void LogEvent(Log log);
        public static LogEvent onLogAdded;

        public GameManager gameManager { get; private set; }
        public LogExperienceController logExperienceController { get; private set; }
        public LogGeneralController logGeneralController { get; private set; }

        private Queue<Log> _logs;

        private void AddLog(Log log)
        {
            _logs.Enqueue(log);
            onLogAdded(log);
        }

        public void AddLog(string message,
            float timeSinceGameStart,
            Log.LogType logType,
            Log.LogOrigin logOrigin,
            ISource source)
        {
            var log = new Log(message, timeSinceGameStart, Color.black, source, logType, logOrigin);
            AddLog(log);
        }
 
        public void AddLog(string message,
            float timeSinceGameStart,
            Log.LogType logType,
            Log.LogOrigin logOrigin,
            ISource xpContext,
            Color color)
        {
            var log = new Log(message, timeSinceGameStart, color, xpContext, logType, logOrigin);
            AddLog(log);
        }


        public Log[] GetAllLogs()
        {
            return _logs.ToArray();
        }

        public Log[] GetAllLogs(int n)
        {
            return _logs.Take(n).ToArray();
        }

        public void ClearLogs()
        {
            _logs.Clear();
        }

        public LogManager(GameManager gameManager)
        {
            _logs = new Queue<Log>();
            this.gameManager = gameManager;
            logGeneralController = new LogGeneralController(gameManager, this);
            logExperienceController = new LogExperienceController(gameManager, this);
        }
    }
}