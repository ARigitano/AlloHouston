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
    public class VirtualPlaceholder : MonoBehaviour
    {
        private XPPrefab _currentPrefab;

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