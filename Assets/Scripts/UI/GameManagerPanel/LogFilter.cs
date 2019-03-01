using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience.UI
{
    internal class LogOriginFilter : LogFilter
    {
        public override string logCategoryKey
        {
            get
            {
                return "Origin";
            }
        }

        public LogOriginFilter(string filterName, Func<Log, bool> filter) : base(filterName, filter)
        {
        }
        
    }

    internal class LogTypeFilter : LogFilter
    {
        public override string logCategoryKey
        {
            get
            {
                return "Type";
            }
        }

        public LogTypeFilter(string filterName, Func<Log, bool> filter) : base(filterName, filter)
        {
        }
    }

    [SerializeField]
    internal abstract class LogFilter
    {
        public abstract string logCategoryKey { get; }
        public string filterTextKey { get; protected set; }
        protected Func<Log, bool> _filter;

        public bool enabled = true;

        public virtual bool Filter(Log log)
        {
            return enabled && _filter(log);
        }

        public LogFilter(string filterTextKey, Func<Log, bool> filter)
        {
            this.filterTextKey = filterTextKey;
            this._filter = filter;
        }
    }
}
