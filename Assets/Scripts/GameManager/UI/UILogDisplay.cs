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
        public List<UILog> uiLogs = new List<UILog>();
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
        private Toggle _togglePrefab = null;
        /// <summary>
        /// Transform of the log panel.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform of the log panel.")]
        private Transform _logPanel = null;
        /// <summary>
        /// Transform of the log filter panel.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform of the log filter panel.")]
        private Transform _logFilterPanel = null;

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
            LogController.onLogAdded += OnLogAdded;
        }

        private void OnLogAdded(Log log)
        {
            if (FilterLog(log))
            {
                var go = Instantiate(_logPrefab, _logPanel);
                go.Init(log);
                uiLogs.Add(go);
            }
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            foreach (var filter in _allFilters)
            {
                var go = Instantiate(_togglePrefab, _logFilterPanel);
                go.onValueChanged.AddListener((value) =>
                {
                    filter.enabled = value;
                    RefreshList();
                });
                go.isOn = true;
                go.GetComponentInChildren<Text>().text = filter.filterName;
                go.name = "Toggle " + filter.filterName;
            }
        }

        private bool FilterLog(Log log)
        {
            return _allFilters
                .GroupBy(allFilter => allFilter.GetType())
                .All(filterGroup => filterGroup.Any(filter => filter.Filter(log)));
        }

        private void RefreshList()
        {
            foreach (var log in uiLogs)
            {
                Destroy(log.gameObject);
                Destroy(log);
            }
            uiLogs.Clear();
            Log[] logs = GameManager.instance.GetAllLogs().Where(
                log => FilterLog(log)).Take(_logLimit).ToArray();
            foreach (var log in logs)
            {
                var go = Instantiate(_logPrefab, _logPanel);
                go.Init(log);
                uiLogs.Add(go);
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                var logOrigin = Extensions.RandomEnumValue<Log.LogOrigin>();
                var logType = Extensions.RandomEnumValue<Log.LogType>();
                string name = string.Format("{0} {1}", logType, logOrigin);
                GameManager.instance.AddLog(name, logOrigin, logType);
            }
        }
    }
}
