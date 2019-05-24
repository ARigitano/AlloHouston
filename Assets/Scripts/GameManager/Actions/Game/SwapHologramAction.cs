using CRI.HelloHouston.Audio;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New SwapHologram Action", menuName = "Actions/Game/Swap Hologram")]
    public class SwapHologramAction : GameAction
    {
        [Tooltip("Index of the hologram to swap to.")]
        public int hologramIndex;

        public override void Act(GameManager controller)
        {
            if (controller != null)
                controller.SwapHologram(hologramIndex);
        }
    }
}
