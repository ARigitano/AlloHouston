using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience.UI
{
    public class LogIndicationFilter : LogFilter
    {
        public LogIndicationFilter(string filterName, Func<Log, bool> filter) : base(filterName, filter)
        {
        }
    }

    public class LogContentFilter : LogFilter
    {
        public LogContentFilter(string filterName, Func<Log, bool> filter) : base(filterName, filter)
        {
        }
    }

    public class LogOriginFilter : LogFilter
    {
        public LogOriginFilter(string filterName, Func<Log, bool> filter) : base(filterName, filter)
        {
        }
    }

    public class LogTypeFilter : LogFilter
    {
        public LogTypeFilter(string filterName, Func<Log, bool> filter) : base(filterName, filter)
        {
        }
    }

    [SerializeField]
    public abstract class LogFilter
    {
        public string filterName;
        protected Func<Log, bool> _filter;
        public bool enabled = true;

        public virtual bool Filter(Log log)
        {
            return enabled && _filter(log);
        }

        public LogFilter(string filterName, Func<Log, bool> filter)
        {
            this.filterName = filterName;
            this._filter = filter;
        }
    }
}
