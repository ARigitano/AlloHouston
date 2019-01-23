using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// Possible categories of exits.
    /// </summary>
    public enum ExitType
    {
        γ_γ_q_qBar_μBar_μ,
        q_qBar_μ_μBar_e_vBar,
        vBar_γ_e_eBar_v,
        μ_eBar_v_v_vBar_vBar
    }

    [CreateAssetMenu(fileName = "New Reaction Exit", menuName = "Experience/MAIA/Reaction Exit", order = 1)]
    public class ReactionExit : ScriptableObject
    {
        /// <summary>
        /// Category of the exit.
        /// </summary>
        public ExitType type;
        /// <summary>
        /// Particles in the exit.
        /// </summary>
        public Particle[] particles;
    }
}
