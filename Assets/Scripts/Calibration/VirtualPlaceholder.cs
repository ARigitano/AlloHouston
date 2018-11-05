using UnityEngine;

public class XPPrefab
{

}

public class XPDoor : XPPrefab
{

}

public class XPWallTop : XPPrefab
{

}

public class XPWallBottom : XPPrefab
{

}

public class XPTablet : XPPrefab
{

}

public class XPCorner : XPPrefab
{

}

public class XPHologram : XPPrefab
{

}

namespace CRI.HelloHouston.Calibration
{
    public enum VirtualPlaceholderType
    {
        WallTopLeft,
        WallTopRight,
        Tablet,
        WallBottomLeft,
        WallBottomRight,
        Hologram,
        Corner,
        Door,
        Unknown
    }

    public class VirtualPlaceholder : MonoBehaviour
    {
        private XPPrefab _currentPrefab;
        /// <summary>
        /// The type of placeholder.
        /// </summary>
        [Tooltip("The type of placeholder")]
        public VirtualPlaceholderType placeholderType;

        public void PlaceObject(XPPrefab prefab)
        {
            _currentPrefab = prefab;
        }

        public T GetObject<T>() where T : XPPrefab, new()
        {
            return _currentPrefab as T;
        }
    }
}