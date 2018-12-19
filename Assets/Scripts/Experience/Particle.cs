using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// Constructor for a particle scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New Particle", menuName = "PhysicsParticle/Particle", order = 1)]
    public class Particle : ScriptableObject
    {
        /// <summary>
        /// The name of the particle.
        /// </summary>
        [Tooltip("The name of the particle.")]
        public string particleName;
        /// <summary>
        /// The symbol of the particle.
        /// </summary>
        [Tooltip("The symbol of the particle.")]
        public string symbol;
        /// <summary>
        /// Does the particle have a negative charge?
        /// </summary>
        [Tooltip("Does the particle have a negative charge?")]
        public bool negative;
        /// <summary>
        /// Does the particle display a line during the reaction?
        /// </summary>
        [Tooltip("Does the particle display a line during the reaction?")]
        public bool line;
        /// <summary>
        /// The zone in which the particle will stop during the reaction.
        /// </summary>
        [Tooltip("The zone in which the particle will stop during the reaction.")]
        public int destination;
        /// <summary>
        /// Does the particle display a second line during the reaction?
        /// </summary>
        [Tooltip("Does the particle display a second line during the reaction?")]
        public bool secondLine;
        /// <summary>
        /// The second zone in which a particle with a second line will stop during the reaction.
        /// </summary>
        [Tooltip("The second zone in which a particle with a second line will stop during the reaction.")]
        public int secondDestination;
        /// <summary>
        /// Does the particle display a head during the reaction?
        /// </summary>
        [Tooltip("Does the particle display a head during the reaction?")]
        public bool head;
        /// <summary>
        /// Is the particle line straight?
        /// </summary>
        [Tooltip("Is the line straight?")]
        public bool straight;
        /// <summary>
        /// Does the particle stop at the extremity of its destination?
        /// </summary>
        [Tooltip("Does the particle stop at the extremity of its destination?")]
        public bool extremity;
        /// <summary>
        /// Color at the end of the line, if there's a line.
        /// </summary>
        [Tooltip("Color at the end of the line, if there's a line.")]
        public Color endColor;
    }
}
