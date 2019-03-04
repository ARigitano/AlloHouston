using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

public class UIComTube : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Transform of the object in which the fill items will be displayed.")]
    private Transform _fillGroup;
    [SerializeField]
    [Tooltip("Prefab of the fill items.")]
    private Image _fillPrefab;
    /// <summary>
    /// The manager associated with the tube.
    /// </summary>
    public XPManager _xpManager;
}
