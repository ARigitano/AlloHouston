using System;
using UnityEngine;

namespace VRCalibrationTool
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

        public VirtualPlaceholder<XPCorner> wallCornerPlaceholder;
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

        public VirtualPlaceholder<XPWallTop> wallTopLeftPlaceholder;
        public VirtualPlaceholder<XPWallTop> wallTopRightPlaceholder;
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

        public VirtualPlaceholder<XPWallBottom> wallBottomLeftPlaceholder;
        public VirtualPlaceholder<XPWallBottom> wallBottomRightPlaceholder;
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

        public VirtualPlaceholder<XPDoor> doorPlaceholder;
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
        public VirtualPlaceholder<XPHologram>[] hologramPlaceholders;
    }

    public abstract class VirtualZone : MonoBehaviour
    {
        public abstract VirtualZoneType type { get; }
    }
}
