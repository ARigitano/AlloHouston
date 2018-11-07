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
        /// <summary>
        /// Places an object on a placeholder.
        /// </summary>
        /// <param name="prefab"></param>
        public void PlaceObject(XPPrefab prefab)
        {
            _currentPrefab = prefab;
        }
        /// <summary>
        /// Return the experience prefab stored in the placeholder.
        /// </summary>
        /// <typeparam name="T">The type of XPPrefab</typeparam>
        /// <returns>An instance of XPPrefab</returns>
        public T GetObject<T>() where T : XPPrefab, new()
        {
            return _currentPrefab as T;
        }
    }
}