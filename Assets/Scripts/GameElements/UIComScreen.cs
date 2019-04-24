using CRI.HelloHouston.Experience;
using CRI.HelloHouston.Translation;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.GameElements
{
    public class UIComScreen : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The title text")]
        private TranslatedText _titleText = null;
        [SerializeField]
        [Tooltip("The tube prefab.")]
        private UIComTube _tubePrefab = null;
        [SerializeField]
        [Tooltip("The transform of the tube group.")]
        public Transform _tubeGroupTransform = null;
        [SerializeField]
        [Tooltip("The progress bar object.")]
        public ComScreenProgressBar _progressBar;

        private UIComTube[] _tubes;
        private GameManager _gameManager;
        private ILangManager _langManager;
        private XPManager[] _managers;

        private void OnEnable()
        {
            for (int i = 0; _managers != null && i < _managers.Length; i++)
            {
                _managers[i].onStateChange += OnManagerStateChange;
                _managers[i].onVisibilityChange += OnManageerVisibilityChange;
            }
            if (_langManager != null)
                _langManager.langManager.onLangChange += OnLangChange;
        }
        
        private void OnDisable()
        {
            for (int i = 0; _managers != null && i < _managers.Length; i++)
            {
                _managers[i].onStateChange -= OnManagerStateChange;
                _managers[i].onVisibilityChange -= OnManageerVisibilityChange;
            }
            if (_langManager != null)
                _langManager.langManager.onLangChange -= OnLangChange;
        }

        public void Init(GameManager gameManager, XPManager[] managers)
        {
            int count = managers.Length;
            _gameManager = gameManager;
            _tubes = new UIComTube[count];
            _managers = new XPManager[count];
            _langManager = _titleText.manager;
            for (int i = 0; i < count; i++)
            {
                XPManager manager = managers[i];
                _tubes[i] = Instantiate(_tubePrefab, _tubeGroupTransform);
                _tubes[i].Init(manager);
                _managers[i] = manager;
                manager.onStateChange += OnManagerStateChange;
                manager.onVisibilityChange += OnManageerVisibilityChange;           
            }
            _langManager.langManager.onLangChange += OnLangChange;
            _progressBar.Init(gameManager, managers);
            UpdateText();
        }

        private void OnManageerVisibilityChange(object sender, XPManagerEventArgs e)
        {

        }

        private void OnLangChange(object sender, LangManagerEventArgs e)
        {
            UpdateText();
        }

        private void OnManagerStateChange(object sender, XPManagerEventArgs e)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            int count = _managers.Count(manager => manager.state == XPState.InProgress || manager.state == XPState.Success);
            string text = _langManager.textManager.GetText(_titleText.textKey);
            string replacedText = text.Replace("[p]", count.ToString());
            _titleText.GetComponent<Text>().text = replacedText;
        }
    }
}
