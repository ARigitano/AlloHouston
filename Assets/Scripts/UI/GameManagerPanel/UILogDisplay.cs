using CRI.HelloHouston.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    internal class UILogDisplay : MonoBehaviour
    {
        [Serializable]
        internal class LogSettings
        {
            public Log.LogType logType;
            public Color color;
            public string textKey;
        
            public LogSettings(Log.LogType logType, Color color, string textKey)
            {
                this.logType = logType;
                this.color = color;
                this.textKey = textKey;
            }
        }
        [Serializable]
        internal class LogOriginSettings
        {
            public Log.LogOrigin logOriginType;
            public string textKey;

            public LogOriginSettings(Log.LogOrigin logOriginType, string textKey)
            {
                this.logOriginType = logOriginType;
                this.textKey = textKey;
            }
        }
        /// <summary>
        /// The limit of log. If the number of logs goes over this value, the log will delete the oldest logs.
        /// </summary>
        [SerializeField]
        [Tooltip("The limit of log. If the number of log goes over this value, the log will delete the oldest logs.")]
        private int _logLimit = 100;
        /// <summary>
        /// The limit of log. If the number of logs goes over this value, the log will delete the oldest logs.
        /// </summary>
        public int logLimig
        {
            get
            {
                return _logLimit;
            }
        }

        public Queue<UILog> uiLogs = new Queue<UILog>();
        /// <summary>
        /// The prefab of the logs.
        /// </summary>
        [SerializeField]
        [Tooltip("The prefab of the logs.")]
        private UILog _logPrefab = null;
        /// <summary>
        /// The prefab of the filter toggle.
        /// </summary>
        [SerializeField]
        [Tooltip("The prefab of the filter toggle.")]
        private UIFilterCategoryPanel _categoryPrefab = null;
        /// <summary>
        /// Transform of the log panel.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform of the log panel.")]
        private Transform _logPanel = null;

        [SerializeField]
        private ScrollRect _scrollRect = null;
        /// <summary>
        /// Transform of the log filter panel.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform of the log filter panel.")]
        private Transform _logFilterPanel = null;
        [SerializeField]
        private string _emptyLogTextKey = "";

        private LogManager _logManager;

        [SerializeField]
        private LogSettings[] _logSettings = null;
        [SerializeField]
        private LogOriginSettings[] _logOriginSettings = null;

        private LogFilter[] _allFilters = new LogFilter[]
        {
            new LogTypeFilter("Default", (x) => x.logType == Log.LogType.Default),
            new LogTypeFilter("Important", (x) => x.logType == Log.LogType.Important),
            new LogTypeFilter("Error", (x) => x.logType == Log.LogType.Error),
            new LogTypeFilter("Automatic", (x) => x.logType == Log.LogType.Automatic),
            new LogTypeFilter("Input", (x) => x.logType == Log.LogType.Input),
            new LogTypeFilter("Hint", (x) => x.logType == Log.LogType.Hint),
            new LogOriginFilter("Experience", (x) => x.logOrigin == Log.LogOrigin.Experience),
            new LogOriginFilter("General", (x) => x.logOrigin == Log.LogOrigin.General),
        };

        private void Reset()
        {
            var logTypes = Enum.GetValues(typeof(Log.LogType));
            _logSettings = new LogSettings[logTypes.Length];
            for (int i = 0; i < _logSettings.Length; i++)
            {
                _logSettings[i] = new LogSettings((Log.LogType)logTypes.GetValue(i), Color.black, "");
            }
        }

        private void OnEnable()
        {
            LogManager.onLogAdded += OnLogAdded;
        }

        private void OnDisable()
        {
            LogManager.onLogAdded -= OnLogAdded;
        }

        private void OnLogAdded(Log log)
        {
            if (FilterLog(log))
            {
                AddLog(log);
            }
        }

        public void Init(LogManager logManager)
        {
            _logManager = logManager;
            _allFilters = InitAllFilters(_logSettings, _logOriginSettings);
            foreach (var category in _allFilters.GroupBy(x => x.logCategoryKey,
                (key, group) => new { CategoryName = key, Filters = group.ToArray() }))
            {
                var go = Instantiate(_categoryPrefab, _logFilterPanel);
                go.Init(this, category.Filters, category.CategoryName);
            }
        }

        private LogFilter[] InitAllFilters(LogSettings[] logSettings, LogOriginSettings[] logOriginSettings)
        {
            LogFilter[] res = new LogFilter[logSettings.Length + logOriginSettings.Length];
            for (int i = 0; i < logSettings.Length; i++)
                res[i] = new LogTypeFilter(logSettings[i].textKey, (logFilter) => logFilter.logType == logSettings[i].logType);
            for (int i = 0; i < logOriginSettings.Length; i++)
                res[i + logSettings.Length] = new LogOriginFilter(logOriginSettings[i].textKey, (logFilter) => logFilter.logOrigin == logOriginSettings[i].logOriginType);
            return res;
        }

        private bool FilterLog(Log log)
        {
            return _allFilters
                .GroupBy(allFilter => allFilter.GetType())
                .All(filterGroup => filterGroup.Any(filter => filter.Filter(log)));
        }

        private void AddLog(Log log)
        {
            var logColor = _logSettings.FirstOrDefault(x => x.logType == log.logType);
            if (logColor != null)
                log.color = logColor.color;
            bool scrollToBottom = false;
            scrollToBottom = _scrollRect.normalizedPosition.y < 0.2f;
            if (uiLogs.Count == 1 && uiLogs.Peek().log.message == null)
            {
                var uiLog = uiLogs.Dequeue();
                Destroy(uiLog.gameObject);
                Destroy(uiLog);
            }
            var go = Instantiate(_logPrefab, _logPanel);
            go.Init(log);
            uiLogs.Enqueue(go);
            if (uiLogs.Count > _logLimit)
            {
                var uiLog = uiLogs.Dequeue();
                Destroy(uiLog.gameObject);
                Destroy(uiLog);
            }
            if (scrollToBottom)
                _scrollRect.ScrollToBottom();
        }
        
        public void RefreshList()
        {
            foreach (var log in uiLogs)
            {
                Destroy(log.gameObject);
                Destroy(log);
            }
            uiLogs.Clear();
            Log[] logs = _logManager.GetAllLogs().Where(
                log => FilterLog(log)).Reverse().Take(_logLimit).Reverse().ToArray();
            if (logs.Length == 0)
            {
                var go = Instantiate(_logPrefab, _logPanel);
                go.Init(TextManager.instance.GetText(_emptyLogTextKey));
                uiLogs.Enqueue(go);
            }
            foreach (var log in logs)
            {
                AddLog(log);
            }
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                var logOrigin = EnumExtensions.RandomEnumValue<Log.LogOrigin>();
                var logType = EnumExtensions.RandomEnumValue<Log.LogType>();
                string name = string.Format("{0} {1}", logType, logOrigin);
                if (logOrigin == Log.LogOrigin.Experience)
                    _logManager.logExperienceController.AddLog(name, null, logType);
                else
                    _logManager.logGeneralController.AddLog(name, null, logType);
            }
        }
#endif
    }
}
