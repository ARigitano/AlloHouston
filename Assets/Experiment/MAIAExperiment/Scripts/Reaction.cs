using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// Constructor for a reaction scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New Reaction", menuName = "Experience/MAIA/NewReaction", order = 2)]
    public class Reaction : ScriptableObject
    {
        /// <summary>
        /// Type of entries of the reaction.
        /// </summary>
        public EntryType entries;

        /// <summary>
        /// Type of exits of the reaction.
        /// </summary>
        public ReactionExit exit;

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
    public enum EntryType
    {
        gluon_gluon_fusion,
        top_top_bar_fusion,
        Higgs_strahlung,
        W_Z_fusion,
    }
}
