using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New StopMusic Action", menuName = "Actions/Game/Stop Music")]
    public class StopMusicAction : GameAction
    {
        public override void Act(GameManager controller)
        {
            controller.StopMusic();
        }
    }
}