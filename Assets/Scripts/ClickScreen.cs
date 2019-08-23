using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickScreen : MonoBehaviour
{
    public float rayLength;
    public LayerMask layermask;
    public Camera camera;
    [SerializeField]
    private Slider _selectedSlider;
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

    // Start is called before the first frame update
    void Start()
    {

    }

        // Update is called once per frame
        void Update()
    {

        if(Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
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

        if (_selectedSlider != null)
        {
            for(int i = 0; i< _selectedSlider.maxValue; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    _selectedSlider.value = i+1;
                    break;
                }
            }
        }
    }
}
