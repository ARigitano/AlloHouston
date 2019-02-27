using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Translation
{
    public class UILangPanel : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The language dropdown.")]
        private Dropdown _langDropdown;

        private void Reset()
        {
            _langDropdown = GetComponentInChildren<Dropdown>();
        }

        private void Start()
        {
            LangManager langManager = GameManager.instance.langManager;
            InitDropdown(langManager);
        }

        private void InitDropdown(LangManager langManager)
        {
            _langDropdown.options.Clear();
            foreach (LangApp lang in langManager.langAppAvailable)
            {
                _langDropdown.options.Add(new Dropdown.OptionData { image = lang.sprite, text = lang.languageName });
                _langDropdown.onValueChanged.AddListener((int value) =>
                {
                    langManager.ChangeLang(value);
                });
            }
        }
    }
}
