using CRI.HelloHouston.Audio;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New HideHologram Action", menuName = "Actions/Game/Hide Hologram")]
    public class HideHologramAction : GameAction
    {
        public override void Act(GameManager controller)
        {
            if (controller != null)
                controller.HideHologram();
        }
    }
}
