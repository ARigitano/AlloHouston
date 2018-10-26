using System.Collections;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// Constructor for XpDifficulty scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New XpDifficulty", menuName = "Experience/New XpDifficulty", order = 2)]
    public class XpDifficulty : ScriptableObject
    {
        public enum AudienceType
        {
            Casual,
            Scientific
        }

        public enum DifficultyType
        {
            Easy,
            Medium,
            Hard
        }
        [Tooltip("The name of the experiment. The same for all difficiculties and audiences versions of the experiment.")]
        public string name;                                   //The name of the experiment. The same for all difficiculties and audiences versions of the experiment.

        [Tooltip("The audience of this version of the experiment. Either mainstream public or researchers.")]
        public AudienceType audienceType;                    //The audience of this version of the experiment. Either mainstream public or researchers.

        [Tooltip("The difficulty of this version of the experiment. Either easy, medium or hard.")]
        public DifficultyType difficultyType;               //The difficulty of this version of the experiment. Either easy, medium or hard.

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
