using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.ParticlePhysics
{
    /// <summary>
    /// Constructor for a reaction scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New Reaction", menuName = "PhysicsParticle/NewReaction", order = 2)]
    public class Reaction : ScriptableObject
    {
        /// <summary>
        /// List of entries of the reaction.
        /// </summary>
        public List<entriesType> entries;

        /// <summary>
        /// List of exits of the reaction.
        /// </summary>
        public List<exitsType> exits;

        /// <summary>
        /// Is the reaction fundamental?
        /// </summary>
        public bool fundamental;
    }

    /// <summary>
    /// Possible categories of entries.
    /// </summary>
    public enum entriesType
    {
        blabla,
        blibli,
        blublu, 
        bloblo
    }

    /// <summary>
    /// Possible categories of exits.
    /// </summary>
    public enum exitsType
    {
        blabla,
        blibli,
        blublu,
        bloblo,
        blabla2,
        blibli2,
        blublu2,
        bloblo2
    }
}
