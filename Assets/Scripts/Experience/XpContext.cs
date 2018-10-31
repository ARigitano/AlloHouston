using System.Collections;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// Constructor for XpDifficulty scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New XpContext", menuName = "Experience/New XpContext", order = 2)]
    public class XpContext : ScriptableObject
    {
        [Tooltip("The name of the experiment. The same for all difficiculties and audiences versions of the experiment.")]
        public string name;                                   //The name of the experiment. The same for all difficiculties and audiences versions of the experiment.

        [Tooltip("A description of the audience for the experiment.")]
        public string description;                           //A description of the audience for the experiment.

        [Tooltip("The audience of this version of the experiment. Either mainstream public or researchers.")]
        public string context;                              //The context(audience -ie mainstream or researchers-, physical place, event...) of this version of the experiment. 

        [Tooltip("The estimated duration (in minutes) of this version of the experiment.")]
        public int duration;                                 //The estimated duration (in minutes) of this version of the experiment.

        [Tooltip("An empty object with the XpSynchronizer inhreting script of the experiment.")]
        public XpSynchronizer xpSynchronizer;               //An empty object with the XpSynchronizer inhreting script of the experiment.

        [Tooltip("The screen, window and tablet on the top part of the wall, to interact with the experiment.")]
        public WallTopZonePrefab wallTopZonePrefab;         //The screen, window and tablet on the top part of the wall, to interact with the experiment.

        [Tooltip("The bottom part of the wall, to add additional elements linked to the experiment.")]
        public WallBottomZonePrefab wallBottomZonePrefab;   //The bottom part of the wall, to add additional elements linked to the experiment.

        [Tooltip("The holograms linked to the experiment, to be displayed on the table.")]
        public HologramZonePrefab hologramZonePrefab;       //The holograms linked to the experiment, to be displayed on the table.

        [Tooltip("The corner zones on each side of the modules, to display static elements linked to the experiment.")]
        public CornerZonePrefab cornerZonePrefab;           //The corner zones on each side of the modules, to display static elements linked to the experiment.

        [Tooltip("The door at the entrance of the room.")]
        public DoorZonePrefab doorZonePrefab;               //The door at the entrance of the room.
    }
}
