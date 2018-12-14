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
        public entriesType entries;

        /// <summary>
        /// List of exits of the reaction.
        /// </summary>
        public exitsType exits;

        /// <summary>
        /// Is the reaction fundamental?
        /// </summary>
        public bool fundamental;

        /// <summary>
        /// Image of the Feynman diagram.
        /// </summary>
        public Texture diagramImage;
    }

    /// <summary>
    /// Possible categories of entries.
    /// </summary>
    public enum entriesType
    {
        gluon_gluon_fusion,
        top_top_bar_fusion,
        Higgs_strahlung,
        W_Z_fusion
    }

    /// <summary>
    /// Possible categories of exits.
    /// </summary>
    public enum exitsType
    {
       γ_γ_q_qBar_μBar_μ,
       q_qBar_μ_μBar_e_vBar,
       vBar_γ_e_eBar_v,
       μ_eBar_v_v_vBar_vBar
    }
}
