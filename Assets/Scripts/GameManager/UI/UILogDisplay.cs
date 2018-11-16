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
            new LogOriginFilter("Experience", (x) => x.logOrigin == Log.LogOrigin.Experience),
            new LogOriginFilter("General", (x) => x.logOrigin == Log.LogOrigin.General),
            new LogContentFilter("Automatic", (x) => x.logContent == Log.LogContent.Automatic),
            new LogContentFilter("Input", (x) => x.logContent == Log.LogContent.Input),
            new LogIndicationFilter("GM Indication", (x) => x.gmIndication),
        };

        private void OnEnable()
        {
            LogController.onLogAdded += OnLogAdded;
        }

        private void OnLogAdded(Log log)
        {
            if (_allFilters.GroupBy(allFilter => allFilter.GetType()).All(
                    filterGroup => filterGroup.Any(filter => filter.Filter(log))))
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
                go.GetComponentInChildren<Text>().text = filter.filterName;
                go.name = "Toggle " + filter.filterName;
            }
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
                log => _allFilters.GroupBy(allFilter => allFilter.GetType()).All(
                    filterGroup => filterGroup.Any(filter => filter.Filter(log)))
                ).Take(_logLimit).ToArray();
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
                GameManager.instance.AddLog("TESTTESTTEST", Extensions.RandomEnumValue<Log.LogOrigin>(), Extensions.RandomEnumValue<Log.LogContent>(), Extensions.RandomEnumValue<Log.LogType>(), UnityEngine.Random.Range(0, 2) == 1);
        }
    }
}
