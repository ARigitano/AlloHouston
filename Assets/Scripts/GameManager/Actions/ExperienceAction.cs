using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRI.HelloHouston.Experience.Actions
{
    public abstract class ExperienceAction : GeneralAction
    {
        public abstract void Act(XPManager controller);
    }
}
