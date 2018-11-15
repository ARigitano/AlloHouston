using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// Constructor for XpDifficulty scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New XpContext", menuName = "Experience/XpContext", order = 2)]
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
        public XPMainSynchronizer xpSynchronizer;

        /// <summary>
        /// The screen, window and tablet on the top part of the wall, to interact with the experiment.
        /// </summary>
        [Tooltip("The screen, window and tablet on the top part of the wall, to interact with the experiment.")]
        public XPWallTopZone xpWallTopZone;

        /// <summary>
        /// The bottom part of the wall, to add additional elements linked to the experiment.
        /// </summary>
        [Tooltip("The bottom part of the wall, to add additional elements linked to the experiment.")]
        public XPWallBottomZone[] xpWallBottomZones;

        /// <summary>
        /// The holograms linked to the experiment, to be displayed on the table.
        /// </summary>
        [Tooltip("The holograms linked to the experiment, to be displayed on the table.")]
        public XPHologramZone xpHologramZone;

        /// <summary>
        /// The corner zones on each side of the modules, to display static elements linked to the experiment.
        /// </summary>
        [Tooltip("The corner zones on each side of the modules, to display static elements linked to the experiment.")]
        public XPCornerZone[] xpCornerZone;

        /// <summary>
        /// The door at the entrance of the room.
        /// </summary>
        [Tooltip("The door at the entrance of the room.")]
        public XPDoorZone xpDoorZone;     
        
        public int totalWallTop
        {
            get
            {
                return xpWallTopZone.elementPrefabs.Length != 0 ? 1 : 0;
            }
        }      
        
        public int totalWallBottom
        {
            get
            {
                return xpWallBottomZones.Count(x => x.elementPrefabs.Length != 0);
            }
        }    

        public int totalCorners
        {
            get
            {
                return xpCornerZone.Count(x => x.elementPrefabs.Length != 0);
            }
        }

        public int totalDoors
        {
            get
            {
                return xpDoorZone.elementPrefabs.Length != 0 ? 1 : 0;
            }
        }

        public int totalHolograms
        {
            get
            {
                return xpHologramZone.elementPrefabs.Length;
            }
        }

        public List<XPZone> zones
        {
            get
            {
                var res = new List<XPZone>();
                if (xpWallTopZone != null)
                    res.Add(xpWallTopZone);
                res = res.Concat(xpWallBottomZones).ToList();
                if (xpHologramZone != null)
                    res.Add(xpHologramZone);
                res = res.Concat(xpCornerZone).ToList();
                if (xpDoorZone != null)
                    res.Add(xpDoorZone);
                return res;
            }
        }
    }
}
