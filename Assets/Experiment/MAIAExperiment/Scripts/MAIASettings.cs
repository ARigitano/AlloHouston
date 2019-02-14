using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    [CreateAssetMenu(fileName = "New MAIASettings", menuName = "Experience/MAIA/MAIASettings", order = 3)]
    public class MAIASettings : XPSettings
    {
        /// <summary>
        /// All the particle scriptable objects.
        /// </summary>
        public Particle[] allParticles;
        /// <summary>
        /// All the reaction scriptable objects.
        /// </summary>
        public Reaction[] allReactions;
        /// <summary>
        /// Password to get access.
        /// </summary>
        [Tooltip("Password to get access.")]
        public string password;
        /// <summary>
        /// Number of ongoing reactions.
        /// </summary>
        [Tooltip("Number of ongoing reactions.")]
        public int reactionCount = 4;
    }
}
