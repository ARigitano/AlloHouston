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

        private List<Log.LogType> _filters = new List<Log.LogType>();

        private void OnEnable()
        {
            LogController.onLogAdded += OnLogAdded;
        }

        private void OnLogAdded(Log log)
        {
            if (log.logType.All(x => _filters.Any(filter => filter == x)))
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
            foreach (var filter in Enum.GetValues(typeof(Log.LogType)))
            {
                var go = Instantiate(_togglePrefab, _logFilterPanel);
                go.onValueChanged.AddListener((value) =>
                {
                    if (value)
                        AddFilter((Log.LogType)filter);
                    else
                        RemoveFilter((Log.LogType)filter);
                });
                go.isOn = false;
                go.GetComponentInChildren<Text>().text = filter.ToString();
                go.name = "Toggle " + filter.ToString();
            }
        }

        private void AddFilter(Log.LogType logType)
        {
            _filters.Add(logType);
            RefreshList();
        }

        private void RemoveFilter(Log.LogType logType)
        {
            _filters.Remove(logType);
            RefreshList();
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
                log => log.logType.All(x => _filters.Any(filter => filter == x))
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
            {
                var logTypes = new List<Log.LogType>();
                for (int i = 0; i < UnityEngine.Random.Range(0, 5); i++)
                {
                    logTypes.Add(Extensions.RandomEnumValue<Log.LogType>());
                }
                string logStr = logTypes.Select(x => x.ToString()).Aggregate((x, y) => x + " " + y);
                Debug.Log(logStr);
                GameManager.instance.AddLog(logStr, logTypes.ToArray());
            }
        }
    }
}
