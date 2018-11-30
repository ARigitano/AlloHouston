using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRI.HelloHouston.Experience
{
    public struct GameHint
    {
        public string hint { get; private set; }
        public ISource source { get; private set; }

        public GameHint(string hint, ISource source)
        {
            this.hint = hint;
            this.source = source;
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", source.sourceName, hint);
        }
    }
}
