using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public struct Log
    {
        public enum LogType
        {
            Error,
            Important,
            Default
        }

        public enum LogOrigin
        {
            General,
            Experience
        }

        public enum LogContent
        {
            Input,
            Automatic
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
        /// The name of the xpContext.
        /// </summary>
        public XPContext xpContext;
        /// <summary>
        /// The log type.
        /// </summary>
        public LogType logType;
        /// <summary>
        /// The log origin.
        /// </summary>
        public LogOrigin logOrigin;
        /// <summary>
        /// The log contenu.
        /// </summary>
        public LogContent logContent;
        /// <summary>
        /// True if the log is an indication for the gm.
        /// </summary>
        public bool gmIndication;

        public Log(string message, float time, Color color, XPContext xpContext, LogType logType, LogOrigin logOrigin, LogContent logContent, bool gmIndication)
        {
            this.message = message;
            this.time = time;
            this.color = color;
            this.xpContext = xpContext;
            this.logType = logType;
            this.logOrigin = logOrigin;
            this.logContent = logContent;
            this.gmIndication = gmIndication;
        }

        private string TimeFormat(float time)
        {
            return string.Format("[{0}:{1:00}]", (int)(time / 60), (int)(time % 60));
        }

        private string ContextNameFormat(XPContext xpContext)
        {
            return string.Format("[{0}]", xpContext ? xpContext.xpGroup.experimentName.ToUpper() : "Room");
        }

        public string ToString(bool displayTime)
        {
            if (displayTime)
                return this.ToString();
            return string.Format("<color=#{0}>{1} - {2}</color>", ColorUtility.ToHtmlStringRGB(color), ContextNameFormat(xpContext), message);
        }

        public override string ToString()
        { 
            return string.Format("<color=#{0}>{1}{2} - {3}</color>", ColorUtility.ToHtmlStringRGB(color), TimeFormat(time), ContextNameFormat(xpContext), message);
        }
    }

    public class LogController
    {
        public delegate void LogEvent(Log log);
        public static LogEvent onLogAdded;

        public GameManager gameManager { get; private set; }

        public Queue<Log> logs { get; private set; }

        private void AddLog(Log log)
        {
            Debug.Log(log);
            logs.Enqueue(log);
            onLogAdded(log);
        }

        public void AddLog(string message,
            float timeSinceGameStart,
            Log.LogType logType,
            Log.LogOrigin logOrigin,
            Log.LogContent logContent,
            bool gmIndication = false,
            XPContext xpContext = null)
        {
            var log = new Log(message, Time.time - timeSinceGameStart, Color.black, xpContext, logType, logOrigin, logContent, gmIndication);
            AddLog(log);
        }
 
        public void AddLog(string message,
            float timeSinceGameStart,
            Log.LogType logType,
            Log.LogOrigin logOrigin,
            Log.LogContent logContent,
            bool gmIndication,
            XPContext xpContext,
            Color color)
        {
            var log = new Log(message, Time.time - timeSinceGameStart, color, xpContext, logType, logOrigin, logContent, gmIndication);
            AddLog(log);
        }

        public LogController(GameManager gameManager)
        {
            logs = new Queue<Log>();
            this.gameManager = gameManager;
        }
    }
}