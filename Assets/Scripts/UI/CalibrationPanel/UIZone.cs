using System;
using CRI.HelloHouston.Calibration;
using UnityEngine;
using CRI.HelloHouston.Experience;
using UnityEngine.UI;

namespace CRI.HelloHouston.Calibration.UI
{
    internal class UIZone : MonoBehaviour, IPointerClickable
    {
        /// <summary>
        /// An instance of virtual zone.
        /// </summary>
        [SerializeField]
        [Tooltip("An instance virtual zone.")]
        private VirtualZone _virtualZone;

        /// <summary>
        /// An instance of virtual zone.
        /// </summary>
        public XPZone xpZone
        {
            get
            {
                return _virtualZone.xpZone;
            }
        }

        public XPContext xpContext
        {
            get
            {
                return _virtualZone.xpContext;
            }
        }

        public ZoneType zoneType
        {
            get
            {
                return _virtualZone.zoneType;
            }
        }
        /// <summary>
        /// Text where the name of the current xpZone will be displayed.
        /// </summary>
        [SerializeField]
        [Tooltip("Text where the name of the current xpZone will be displayed.")]
        private Text _text;
        /// <summary>
        /// Background of the text.
        /// </summary>
        [SerializeField]
        [Tooltip("Background of the text.")]
        private Image _textBackground;
        /// <summary>
        /// Material hen the zone is selected.
        /// </summary>
        [SerializeField]
        [Tooltip("Material when the zone is selected.")]
        private Material _selectedMaterial = null;
        /// <summary>
        /// Material when the zone is hovered.
        /// </summary>
        [SerializeField]
        [Tooltip("Material when the zone is hovered.")]
        private Material _hoveredMaterial = null;
        /// <summary>
        /// Material when the zone is not selected.
        /// </summary>
        [SerializeField]
        [Tooltip("Material when the zone is not selected.")]
        private Material _unselectedMaterial = null;

        private ZoneManager _zoneManager;
        /// <summary>
        /// Text when the virtual zone is empty.
        /// </summary>
        [SerializeField]
        [Tooltip("Text when the virtual zone is empty.")]
        private string _emptyText = "<Empty>";
        /// <summary>
        /// Color of the text's background when the zone is empty.
        /// </summary>
        [SerializeField]
        [Tooltip("Color of the text's background when the zone is empty.")]
        private Color _emptyBackgroundColor = Color.black;
        /// <summary>
        /// Color of the text's background when the zone is full.
        /// </summary>
        [SerializeField]
        [Tooltip("Color of the text's background when the zone is full.")]
        private Color _fullBackgroundColor = Color.blue;

        private bool _selected = false;

        private bool _enter = false;

        public void Init(ZoneManager zoneManager)
        {
            _zoneManager = zoneManager;
            _text.text = _emptyText;
            _textBackground.color = new Color(_emptyBackgroundColor.r, _emptyBackgroundColor.g, _emptyBackgroundColor.b);
        }

        public void Place(XPZone xpZone, XPContext xpContext)
        {
            _virtualZone.Place(xpZone, xpContext);
            _text.text = xpZone == null ? _emptyText : string.Format("{0} {1}", xpContext.contextName, xpZone.name);
            Color color = xpZone == null ? _emptyBackgroundColor : _fullBackgroundColor;
            _textBackground.color = new Color(color.r, color.g, color.b);
        }

        public void Clean()
        {
            Place(null, null);
        }

        public void Unselect()
        {
            _selected = false;
            if (!_enter)
                GetComponent<Renderer>().material = _unselectedMaterial;
            else
                GetComponent<Renderer>().material = _hoveredMaterial;
        }

        public void Select()
        {
            _selected = true;
            if (_zoneManager != null)
                GetComponent<Renderer>().material = _selectedMaterial;
        }

        void IPointerClickable.OnLaserClick()
        {
        }

        void IPointerClickable.OnLaserEnter()
        {
            if (_zoneManager != null && _zoneManager.IsSelectable(this) && !_selected)
                GetComponent<Renderer>().material = _hoveredMaterial;
            _enter = true;
        }

        void IPointerClickable.OnLaserExit()
        {
            if (_zoneManager != null && !_selected)
                GetComponent<Renderer>().material = _unselectedMaterial;
            _enter = false;
        }

        private void Reset()
        {
            _virtualZone = GetComponent<VirtualZone>();
            _text = GetComponentInChildren<Text>();
            _textBackground = GetComponentInChildren<Image>();
        }
    }
}
