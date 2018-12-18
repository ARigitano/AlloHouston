using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// Constructor for XpGroup scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New XpGroup", menuName = "Experience/XpGroup", order = 1)]
    public class XPGroup : ScriptableObject
    {
        /// <summary>
        /// The name of the experiment.
        /// </summary>
        [Tooltip("The name of the experiment.")]
        public string experimentName;

        /// <summary>
        /// A description of the experiment.
        /// </summary>
        [Tooltip("A description of the experiment.")]
        public string description;

        /// <summary>
        /// The type of gameplay of the experiment.
        /// </summary>
        [Tooltip("The type of gameplay of the experiment.")]
        public string type;

        /// <summary>
        /// The pedagogical content of the experiment.
        /// </summary>
        [Tooltip("The pedagogical content of the experiment.")]
        public string subject;

        /// <summary>
        /// An int to identify the experiment.
        /// </summary>
        [Tooltip("An int to identify the experiment.")]
        public int id;
    }
}