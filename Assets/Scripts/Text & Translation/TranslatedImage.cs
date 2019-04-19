using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Translation
{
    public struct TranslatedImageEventArgs
    {
        public LangApp lang;
        public Sprite sprite;
    }

    public delegate void TranslatedImageEventHandler(object sender, TranslatedImageEventArgs e);

    [RequireComponent(typeof(Image))]
    public abstract class TranslatedImage : MonoBehaviour
    {
        public event TranslatedImageEventHandler onLangChange;
        [Tooltip("An instance of translated image asset.")]
        [SerializeField]
        protected TranslatedImageAsset _translatedImageAsset;

        /// <summary>
        /// An instance of a translated image asset.
        /// </summary>
        public TranslatedImageAsset translatedImageAsset
        {
            get
            {
                return _translatedImageAsset;
            }
        }

        /// <summary>
        /// The image component.
        /// </summary>
        [SerializeField]
        [Tooltip("The image component.")]
        private Image _image;

        /// <summary>
        /// If true, the translated text image find a manager by itself at start.
        /// </summary>
        [SerializeField]
        [Tooltip("If true, the translated image will find a manager by itself at start.")]
        protected bool _autoInit = true;

        protected bool _initialized = false;
        /// <summary>
        /// If true, this translated text has already been initialized once.
        /// </summary>
        public bool initialized
        {
            get
            {
                return _initialized;
            }
        }

        [SerializeField]
        [Tooltip("The lang manager. If this field is empty, the TranslatedText component will find a suitable LangManager automatically. (recommended)")]
        protected ILangManager _manager;

        private void OnEnable()
        {
            if (_manager != null)
                _manager.langManager.onLangChange += OnLangChange;
        }

        private void OnDisable()
        {
            if (_manager != null)
                _manager.langManager.onLangChange -= OnLangChange;
        }

        private void Reset()
        {
            _image = GetComponent<Image>();
        }

        /// <summary>
        /// Called whenever the OnLangChange event of the LangManager is triggered. Sets the image to its current lang value.
        /// </summary>
        /// <param name="lang"></param>
        private void OnLangChange(LangApp lang)
        {
            SetImage(_manager, lang);
            if (onLangChange != null)
            {
                LangImage la = _translatedImageAsset.GetCurrentImage(lang);
                Sprite sprite = null;
                if (la != null)
                    sprite = la.image;
                onLangChange(this, new TranslatedImageEventArgs() { lang = lang, sprite = sprite });
            }
        }

        /// <summary>
        /// Init the translated image.
        /// </summary>
        /// <param name="manager">The lang manager.</param>
        /// <param name="isCommon">Is the text common ?</param>
        public void InitTranslatedText(ILangManager manager, TranslatedImageAsset translatedImageAsset)
        {
            _manager = manager;
            _translatedImageAsset = translatedImageAsset;
            Init(manager);
        }
        /// <summary>
        /// Init the translated image.
        /// </summary>
        /// <param name="textManager">The text manager.</param>
        public void Init(ILangManager manager)
        {
            _initialized = true;
            _manager = manager;
            if (_manager.langManager != null)
                _manager.langManager.onLangChange += OnLangChange;
            _image = GetComponent<Image>();
            SetImage(manager, manager.langManager.currentLang);
        }

        /// <summary>
        /// Set the text to its translated value.
        /// </summary>
        private void SetImage(ILangManager manager, LangApp lang)
        {
            if (_translatedImageAsset != null)
            {
                LangImage langImage = _translatedImageAsset.GetCurrentImage(lang);
                if (langImage != null)
                    _image.sprite = langImage.image;
                else
                {
                    langImage = _translatedImageAsset.GetCurrentImage(manager.langManager.defaultLanguage);
                    if (langImage != null)
                        _image.sprite = langImage.image;
                }
            }
        }

        protected abstract void FindManager();

        private void Start()
        {
            if (!_initialized && _autoInit && _manager == null)
            {
                FindManager();
            }
        }
    }
}