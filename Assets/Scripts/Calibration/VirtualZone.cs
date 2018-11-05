using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public enum VirtualZoneType
    {
        Corner,
        WallTop,
        WallBottom,
        Door,
        Hologram,
        Unknown
    }

    public class VirtualCornerZone : VirtualZone
    {
        public override VirtualZoneType type
        {
            get
            {
                return VirtualZoneType.Corner;
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
        public override VirtualZoneType type
        {
            get
            {
                return VirtualZoneType.WallTop;
            }
        }
        public override VirtualPlaceholder[] placeholders
        {
            get
            {
                return new VirtualPlaceholder[] { wallTopLeftPlaceholder, wallTopRightPlaceholder, tabletPlaceholder };
            }
        }

        public VirtualPlaceholder wallTopLeftPlaceholder;
        public VirtualPlaceholder wallTopRightPlaceholder;
        public VirtualPlaceholder tabletPlaceholder;
    }

    public class VirtualWallBottomZone : VirtualZone
    {
        public override VirtualZoneType type
        {
            get
            {
                return VirtualZoneType.WallBottom;
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
        public override VirtualZoneType type
        {
            get
            {
                return VirtualZoneType.Door;
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
        public override VirtualZoneType type
        {
            get
            {
                return VirtualZoneType.Hologram;
            }
        }

        public override VirtualPlaceholder[] placeholders
        {
            get
            {
                return hologramPlaceholders;
            }
        }
        public VirtualPlaceholder[] hologramPlaceholders;
    }

    public abstract class VirtualZone : MonoBehaviour
    {
        public abstract VirtualZoneType type { get; }
        public abstract VirtualPlaceholder[] placeholders { get; }
    }
}