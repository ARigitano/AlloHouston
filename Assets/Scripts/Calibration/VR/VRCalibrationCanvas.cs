using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;

public class VRCalibrationCanvas : MonoBehaviour {
    /// <summary>
    /// An instance of the Vive Controller Manager.
    /// </summary>
    [SerializeField]
    [Tooltip("An instance of the ViveC Controller Manager.")]
    private ViveControllerManager _viveControllerManager = null;
    /// <summary>
    /// Transform of the panel.
    /// </summary>
    [SerializeField]
    [Tooltip("Transform of the panel.")]
    private Transform _panelTransform = null;
    /// <summary>
    /// Button collection prefab.
    /// </summary>
    [SerializeField]
    [Tooltip("Button collection prefab.")]
    private CalibrationButton _buttonCollectionPrefab = null;

    private void Start()
    {
        if (_viveControllerManager == null)
            _viveControllerManager = FindObjectOfType<ViveControllerManager>();
        InitCalibrationCanvas(_viveControllerManager.virtualObjectPrefabs);
    }

    public void InitCalibrationCanvas(VirtualObject[] virtualObjectPrefabs)
    {
        for (int i = 0; i < virtualObjectPrefabs.Length; i++)
        {
            var buttonCollection = Instantiate(_buttonCollectionPrefab, _panelTransform).GetComponent<CalibrationButton>();
            buttonCollection.Init(_viveControllerManager, i, virtualObjectPrefabs[i].name);
        }
    }
}
