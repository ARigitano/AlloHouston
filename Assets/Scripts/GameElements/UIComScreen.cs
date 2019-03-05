using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.GameElements
{
    public class UIComScreen : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The timer text.")]
        private Text _timerText = null;
        [SerializeField]
        [Tooltip("The tube prefab.")]
        private UIComTube _tubePrefab = null;
        [SerializeField]
        [Tooltip("The transform of the tube group.")]
        public Transform _tubeGroupTransform = null;

        private UIComTube[] _tubes;
        private GameManager _gameManager;

        public void Init(GameManager gameManager, XPManager[] managers)
        {
            _gameManager = gameManager;
            _tubes = new UIComTube[managers.Length];
            for (int i = 0; i < managers.Length; i++)
            {
                _tubes[i] = Instantiate(_tubePrefab, _tubeGroupTransform);
                _tubes[i].Init(managers[i]);
            }
        }

        private void Update()
        {
            if (_gameManager != null)
            {
                float timeSinceGameStart = _gameManager.timeSinceGameStart;
                _timerText.text = string.Format("{0:00}:{1:00}",
                    (timeSinceGameStart / 60) % 60,
                    timeSinceGameStart % 60);
            }
        } 
    }
}
