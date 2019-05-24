using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRI.HelloHouston.GameElements
{
    public enum TubexType
    {
        Electricity,
        Aqua,
        Green,
    }
    [CreateAssetMenu(fileName = "New TubexDatabase", menuName = "Settings/Tubex Database")]
    public class TubexDatabase : ScriptableObject
    {
        [Serializable]
        public struct TubexEntry
        {
            public GameObject prefab;
            public TubexType type;
        }
        /// <summary>
        /// An array of prefab-type tubex pairs.
        /// </summary>
        [Tooltip("An array of prefab-type tubex pairs.")]
        public TubexEntry[] tubex;

        public GameObject GetTubex(TubexType type)
        {
            TubexEntry t = tubex.FirstOrDefault(x => x.type == type);
            return t.prefab;
        }
    }
}
