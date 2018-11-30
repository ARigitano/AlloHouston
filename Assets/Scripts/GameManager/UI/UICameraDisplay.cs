using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    internal class UICameraDisplay : MonoBehaviour
    {
        /// <summary>
        /// The image on which the camera view will be displayed.
        /// </summary>
        [SerializeField]
        [Tooltip("The image on which the camera view will be displayed.")]
        private RawImage _image = null;
        /// <summary>
        /// Prefab of the camera button.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab of the camera button.")]
        private Button _buttonPrefab = null;
        /// <summary>
        /// Transform of the button panel on which the buttons are to be displayed.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform of the button panel on which the buttons are to be displayed.")]
        private Transform _buttonPanel = null;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            var cameraGos = GameObject.FindGameObjectsWithTag("DisplayCamera");
            foreach (var cameraGo in cameraGos)
            {
                var camera = cameraGo.GetComponent<Camera>();
                var button = Instantiate(_buttonPrefab, _buttonPanel);
                button.onClick.AddListener(() =>
                {
                    _image.texture = camera.targetTexture;
                });
                button.GetComponentInChildren<Text>().text = camera.name;
            }
        }
    }
}