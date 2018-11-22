using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRI.HelloHouston.Experience
{

    public class LogGeneralController : LogController
    {

        protected override Log.LogOrigin logOrigin
        {
            get
            {
                return Log.LogOrigin.General;
            }
        }
        /// <summary>
        /// Adds a log that will be displayed on the GameManager UI and saved in the log file.
        /// </summary>
        public void AddLog(string str,
            Log.LogType logType = Log.LogType.Default)
        {
            _logManager.AddLog(str, _gameManager.timeSinceGameStart, logType, logOrigin);
        }

        public LogGeneralController(GameManager gameManager, LogManager logManager) : base(gameManager, logManager)
        {
        }
    }

    public class LogExperienceController : LogController
    {

        protected override Log.LogOrigin logOrigin
        {
            get
            {
                return Log.LogOrigin.Experience;
            }
        }

        /// <summary>
        /// Adds a log that will be displayed on the GameManager UI and saved in the log file.
        /// </summary>
        public void AddLog(string str,
            XPContext xpContext,
            Log.LogType logType = Log.LogType.Default)
        {
            _logManager.AddLog(str, _gameManager.timeSinceGameStart, logType, logOrigin, xpContext);
        }

        public LogExperienceController(GameManager gameManager, LogManager logManager) : base(gameManager, logManager)
        {
        }
    }

    public abstract class LogController
    {
        protected LogManager _logManager;
        protected GameManager _gameManager;

        protected abstract Log.LogOrigin logOrigin { get; }

        public LogController(GameManager gameManager, LogManager logManager)
        {
            _gameManager = gameManager;
            _logManager = logManager;
        }
    }
}
