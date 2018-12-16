using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.ParticlePhysics
{
    /// <summary>
    /// Constructor for a particle scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New Particle", menuName = "PhysicsParticle/NewParticle", order = 1)]
    public class Particle : ScriptableObject
    {
        /// <summary>
        /// The name of the particle.
        /// </summary>
        public string particleName;

        /// <summary>
        /// The symbol of the particle.
        /// </summary>
        public string symbol;

        /// <summary>
        /// Does the particle have a negative charge?
        /// </summary>
        public bool negative;

        /// <summary>
        /// Does the particle display a line during the reaction?
        /// </summary>
        public bool line;

        /// <summary>
        /// The zone in which the particle will stop during the reaction.
        /// </summary>
        public int destination;

        /// <summary>
        /// Does the particle display a second line during the reaction?
        /// </summary>
        public bool secondLine;

        /// <summary>
        /// The second zone in which a particle with a second line will stop during the reaction.
        /// </summary>
        public int secondDestination;

        /// <summary>
        /// Does the particle display a head during the reaction?
        /// </summary>
        public bool head;

        /// <summary>
        /// Material used for easier debugging of the reactions.
        /// </summary>
        public Material debugMaterial;

        /// <summary>
        /// Is the particle line straight?
        /// </summary>
        public bool straight;

        /// <summary>
        /// Does the particle stop at the extremity of its destination?
        /// </summary>
        public bool extremity;

        /// <summary>
        /// Image of the symbol of the particle.
        /// </summary>
        public Texture symbolImage;
    }
}
