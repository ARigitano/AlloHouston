using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRCalibrationTool
{
    public class VirtualItem : VirtualObject
    {
        public VirtualPlaceholder[] placeholders;

        public ItemEntry item;

        public void Awake()
        {
            placeholders = GetComponentsInChildren<VirtualPlaceholder>();
        }
    }
}
