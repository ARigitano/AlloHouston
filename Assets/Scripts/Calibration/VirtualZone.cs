using CRI.HelloHouston.Experience;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualCornerZone : VirtualZone
    {
        public override ZoneType type
        {
            get
            {
                return ZoneType.Corner;
            }
        }
        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { wallCornerVirtualElement };
            }
        }

        public VirtualElement wallCornerVirtualElement;
    }

    public class VirtualWallTopZone : VirtualZone
    {
        public override ZoneType type
        {
            get
            {
                return ZoneType.WallTop;
            }
        }
        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { wallTopLeftVirtualElement, wallTopRightVirtualElement, wallTopTabletVirtualElement };
            }
        }

        public VirtualElement wallTopLeftVirtualElement;
        public VirtualElement wallTopRightVirtualElement;
        public VirtualElement wallTopTabletVirtualElement;
    }

    public class VirtualWallBottomZone : VirtualZone
    {
        public override ZoneType type
        {
            get
            {
                return ZoneType.WallBottom;
            }
        }
        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { wallBottomVirtualElement };
            }
        }
        public VirtualElement wallBottomVirtualElement;
    }

    public class VirtualDoorZone : VirtualZone
    {
        public override ZoneType type
        {
            get
            {
                return ZoneType.Door;
            }
        }
        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { doorVirtualElement };
            }
        }

        public VirtualElement doorVirtualElement;
    }

    public class VirtualHologramZone : VirtualZone
    {
        public override ZoneType type
        {
            get
            {
                return ZoneType.Hologram;
            }
        }

        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { hologramVirtualElement };
            }
        }

        public VirtualElement hologramVirtualElement;
    }

    public abstract class VirtualZone : MonoBehaviour
    {
        /// <summary>
        /// Gets the type of virtual zone.
        /// </summary>
        public abstract ZoneType type { get; }
        /// <summary>
        /// Gets all the virtual elements inside the zone.
        /// </summary>
        public abstract VirtualElement[] virtualElements { get; }
    }
}