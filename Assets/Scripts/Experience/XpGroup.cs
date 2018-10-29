using System.Collections;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// Constructor for XpGroup scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New XpGroup", menuName = "Experience/New XpGroup", order = 1)]
    public class XpGroup : ScriptableObject
    {
        [Tooltip("The name of the experiment.")]
        public string name;                //The name of the experiment.

        [Tooltip("A description of the experiment.")]
        public string description;         //A description of the experiment.

        [Tooltip("The type of gameplay of the experiment.")]
        public string type;                //The type of gameplay of the experiment.

        [Tooltip("The pedagogical content of the experiment.")]
        public string subject;             //The pedagogical content of the experiment.
    }
}