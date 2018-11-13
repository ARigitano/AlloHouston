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
        public override VirtualPlaceholder[] placeholders
        {
            get
            {
                return new VirtualPlaceholder[] { wallCornerPlaceholder };
            }
        }

        public VirtualPlaceholder wallCornerPlaceholder;
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
        public override VirtualPlaceholder[] placeholders
        {
            get
            {
                return new VirtualPlaceholder[] { wallTopLeftPlaceholder, wallTopRightPlaceholder, wallTopTabletPlaceholder };
            }
        }

        public VirtualPlaceholder wallTopLeftPlaceholder;
        public VirtualPlaceholder wallTopRightPlaceholder;
        public VirtualPlaceholder wallTopTabletPlaceholder;
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
        public override VirtualPlaceholder[] placeholders
        {
            get
            {
                return new VirtualPlaceholder[] { wallBottomLeftPlaceholder, wallBottomRightPlaceholder };
            }
        }
        public VirtualPlaceholder wallBottomLeftPlaceholder;
        public VirtualPlaceholder wallBottomRightPlaceholder;
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
        public override VirtualPlaceholder[] placeholders
        {
            get
            {
                return new VirtualPlaceholder[] { doorPlaceholder };
            }
        }

        public VirtualPlaceholder doorPlaceholder;
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

        public override VirtualPlaceholder[] placeholders
        {
            get
            {
                return new VirtualPlaceholder[] { hologramPlaceholder };
            }
        }
        public VirtualPlaceholder hologramPlaceholder;
    }

    public abstract class VirtualZone : MonoBehaviour
    {
        /// <summary>
        /// Gets the type of virtual zone.
        /// </summary>
        public abstract ZoneType type { get; }
        /// <summary>
        /// Gets all the placeholders inside the zone.
        /// </summary>
        public abstract VirtualPlaceholder[] placeholders { get; }
    }
}