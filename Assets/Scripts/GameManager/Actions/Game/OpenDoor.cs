using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New OpenDoor Action", menuName = "Actions/Game/OpenDoor")]
    public class OpenDoor : GameAction
    {
        public override void Act(GameManager controller)
        {
            controller.OpenDoor();
        }
    }
}
