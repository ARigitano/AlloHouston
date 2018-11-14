using System.Collections;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// Constructor for XpDifficulty scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New XpContext", menuName = "Experience/New XpContext", order = 2)]
    public class XPContext : ScriptableObject
    {
        /// <summary>
        /// The group of the experiment. The same for all difficiculties and audiences versions of the experiment.
        /// </summary>
        [Tooltip("The group of the experiment. The same for all difficiculties and audiences versions of the experiment.")]
        public XPGroup xpGroup;

        /// <summary>
        /// A description of the audience for the experiment.
        /// </summary>
        [Tooltip("A description of the audience for the experiment.")]
        public string description;

        /// <summary>
        /// The context(audience -ie mainstream or researchers-, physical place, event...) of this version of the experiment. 
        /// </summary>
        [Tooltip("The audience of this version of the experiment. Either mainstream public or researchers.")]
        public string context;

        /// <summary>
        /// The estimated duration (in minutes) of this version of the experiment.
        /// </summary>
        [Tooltip("The estimated duration (in minutes) of this version of the experiment.")]
        public int duration;

        /// <summary>
        /// An empty object with the XpSynchronizer inhreting script of the experiment.
        /// </summary>
        [Tooltip("An empty object with the XpSynchronizer inhreting script of the experiment.")]
        public XPSynchronizer xpSynchronizer;

        /// <summary>
        /// The screen, window and tablet on the top part of the wall, to interact with the experiment.
        /// </summary>
        [Tooltip("The screen, window and tablet on the top part of the wall, to interact with the experiment.")]
        public XPWallTopZone wallTopZonePrefab;

        /// <summary>
        /// The bottom part of the wall, to add additional elements linked to the experiment.
        /// </summary>
        [Tooltip("The bottom part of the wall, to add additional elements linked to the experiment.")]
        public XPWallBottomZone[] wallBottomZonesPrefab;

        /// <summary>
        /// The holograms linked to the experiment, to be displayed on the table.
        /// </summary>
        [Tooltip("The holograms linked to the experiment, to be displayed on the table.")]
        public XPHologramZone hologramZonePrefab;

        /// <summary>
        /// The corner zones on each side of the modules, to display static elements linked to the experiment.
        /// </summary>
        [Tooltip("The corner zones on each side of the modules, to display static elements linked to the experiment.")]
        public XPCornerZone[] cornerZonesPrefab;

        /// <summary>
        /// The door at the entrance of the room.
        /// </summary>
        [Tooltip("The door at the entrance of the room.")]
        public XPDoorZone doorZonePrefab;     
        
        public int totalWallTop
        {
            get
            {
                return wallTopZonePrefab.elementPrefabs.Length != 0 ? 1 : 0;
            }
        }      
        
        public int totalWallBottom
        {
            get
            {
                return wallBottomZonesPrefab.Count(x => x.elementPrefabs.Length != 0);
            }
        }    

        public int totalCorners
        {
            get
            {
                return cornerZonesPrefab.Count(x => x.elementPrefabs.Length != 0);
            }
        }

        public int totalDoors
        {
            get
            {
                return doorZonePrefab.elementPrefabs.Length != 0 ? 1 : 0;
            }
        }

        public int totalHolograms
        {
            get
            {
                return hologramZonePrefab.elementPrefabs.Length;
            }
        }
    }
}
