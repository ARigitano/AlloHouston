using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New CloseDoor Action", menuName = "Actions/Game/CloseDoor")]
    public class CloseDoor : GameAction
    {
        public override void Act(GameManager controller)
        {
            controller.CloseDoor();
        }
    }
}
