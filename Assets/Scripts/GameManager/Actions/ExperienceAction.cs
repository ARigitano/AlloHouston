using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRI.HelloHouston.Experience.Actions
{
    public abstract class ExperienceAction : GeneralAction
    {
        /// <summary>
        /// Applis the action to an ExperienceActionController
        /// </summary>
        /// <param name="controller"></param>
        public abstract void Act(ExperienceActionController controller);
    }
}
