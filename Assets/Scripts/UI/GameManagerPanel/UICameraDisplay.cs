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

        private Camera[] _cameras;

        private void SetCameraActive(Camera camera)
        {
            foreach (var cg in _cameras)
                cg.gameObject.SetActive(cg == camera);
        }
        
        public void Init(Camera[] cameras)
        {
            _cameras = cameras;
            for (int i = 0; i < cameras.Length; i++)
            {
                var camera = cameras[i];
                var button = Instantiate(_buttonPrefab, _buttonPanel);
                button.onClick.AddListener(() =>
                {
                    SetCameraActive(camera);
                });
                button.GetComponentInChildren<Text>().text = camera.name;
                if (i == 0)
                    SetCameraActive(camera);
            }

        }
    }
}