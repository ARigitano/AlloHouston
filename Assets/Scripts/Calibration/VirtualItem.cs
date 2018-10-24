using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRCalibrationTool
{
    public class VirtualItem : VirtualObject
    {
        public VirtualExperienceZone[] placeholders;

        public ItemEntry item;

        public void Awake()
        {
            placeholders = GetComponentsInChildren<VirtualExperienceZone>();
        }
    }
}
