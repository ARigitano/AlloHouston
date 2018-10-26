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

public class XPCorner : XPPrefab
{

}

public class XPHologram : XPPrefab
{

}

public class VirtualPlaceholder<T> where T : XPPrefab
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

    public VirtualPlaceholderType type;

    public void PlaceObject<T>()
    {

    }
}