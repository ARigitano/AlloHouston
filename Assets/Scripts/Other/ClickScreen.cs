using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CRI.HelloHouston
{
    /// <summary>
    /// Allows the player to interact with the ingame UI without a VR headset with the mouse.
    /// </summary>
    public class ClickScreen : MonoBehaviour
    {
        /// <summary>
        /// Length of the camera raycasting.
        /// </summary>
        public float rayLength; 
        /// <summary>
        /// Layermask for the interactable UI.
        /// </summary>
        public LayerMask layermask;
        /// <summary>
        /// The no VR camera.
        /// </summary>
        public Camera camera;
        /// <summary>
        /// The currently selected slider.
        /// </summary>
        [SerializeField]
        private Slider _selectedSlider;
        /// <summary>
        /// Possible entries for a numerical slider.
        /// </summary>
        private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
        };

        // Update is called once per frame
        void Update()
        {
            //Interacting with the UI on mouse click.
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, rayLength, layermask))
                {
                    if (hit.collider.gameObject.GetComponent<Button>() != null)
                        hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    else if (hit.collider.gameObject.GetComponent<Slider>())
                    {
                        _selectedSlider = hit.collider.gameObject.GetComponent<Slider>();
                    }
                }
            }

            //Entering the value of a slider.
            if (_selectedSlider != null)
            {
                for (int i = 0; i < _selectedSlider.maxValue; i++)
                {
                    if (Input.GetKeyDown(keyCodes[i]))
                    {
                        _selectedSlider.value = i + 1;
                        break;
                    }
                }
            }
        }
    }
}
