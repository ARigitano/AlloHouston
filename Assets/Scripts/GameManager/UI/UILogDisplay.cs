using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    public class UILogDisplay : MonoBehaviour
    {
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

        private LogManager _logManager;

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

        private void Start()
        {
            Init(GameManager.instance.logManager);
        }

        public void Init(LogManager logManager)
        {
            _logManager = logManager;
            foreach (var category in _allFilters.GroupBy(x => x.logCategoryKey,
                (key, group) => new { CategoryName = key, Filters = group.ToArray() }))
            {
                var go = Instantiate(_categoryPrefab);
                go.Init(this, category.Filters, category.CategoryName);
                go.transform.SetParent(_logFilterPanel);
            }
        }

        private bool FilterLog(Log log)
        {
            return _allFilters
                .GroupBy(allFilter => allFilter.GetType())
                .All(filterGroup => filterGroup.Any(filter => filter.Filter(log)));
        }

        private void AddLog(Log log)
        {
            bool scrollToBottom = false;
            scrollToBottom = _scrollRect.normalizedPosition.y < 0.1f;
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
                go.Init("There's nothing in this list! Please verify your filters.");
                uiLogs.Enqueue(go);
            }
            foreach (var log in logs)
            {
                AddLog(log);
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                var logOrigin = Extensions.RandomEnumValue<Log.LogOrigin>();
                var logType = Extensions.RandomEnumValue<Log.LogType>();
                string name = string.Format("{0} {1}", logType, logOrigin);
                if (logOrigin == Log.LogOrigin.Experience)
                    _logManager.logExperienceController.AddLog(name, null, logType);
                else
                    _logManager.logGeneralController.AddLog(name, logType);
            }
        }
    }
}
