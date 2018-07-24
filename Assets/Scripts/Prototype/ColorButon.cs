using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;

public class ColorButon : MonoBehaviour
{
    [SerializeField] private ColorXP _colorXP;
    private bool _fixed = false;
    public int index;
    public string inputValue;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("collider");
        if (other.tag == "ViveController" && !_colorXP._fixed) {
            Debug.Log("fixed");
            _colorXP.Resolved(inputValue);
            _colorXP._fixed = true;
		}
	}
}
