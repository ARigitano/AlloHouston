using CRI.HelloHouston.Experience;
using UnityEngine;

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
        private XPContent _currentPrefab;
        /// <summary>
        /// The type of placeholder.
        /// </summary>
        [Tooltip("The type of placeholder")]
        public VirtualPlaceholderType placeholderType;
        /// <summary>
        /// Places an object on a placeholder.
        /// </summary>
        /// <param name="prefab"></param>
        public void PlaceObject(XPContent prefab)
        {
            _currentPrefab = prefab;
        }
        /// <summary>
        /// Return the experience prefab stored in the placeholder.
        /// </summary>
        /// <typeparam name="T">The type of XPPrefab</typeparam>
        /// <returns>An instance of XPPrefab</returns>
        public T GetObject<T>() where T : XPContent, new()
        {
            return _currentPrefab as T;
        }
    }
}