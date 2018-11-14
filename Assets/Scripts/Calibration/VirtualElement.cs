using CRI.HelloHouston.Experience;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public enum ElementType
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

    public class VirtualElement : MonoBehaviour
    {
        private XPElement _currentPrefab;
        /// <summary>
        /// The type of element
        /// </summary>
        [Tooltip("The type of placeholder")]
        public ElementType elementType;
        /// <summary>
        /// Places an experience element on a virtual element.
        /// </summary>
        /// <param name="element"></param>
        public void PlaceObject(XPElement element)
        {
            _currentPrefab = element;
        }
        /// <summary>
        /// Return the experience element stored in the virtual element.
        /// </summary>
        /// <typeparam name="T">The type of XPElement</typeparam>
        /// <returns>An instance of XPElement</returns>
        public T GetObject<T>() where T : XPElement, new()
        {
            return _currentPrefab as T;
        }
    }
}