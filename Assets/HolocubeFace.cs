using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.GameElements
{
    public class HolocubeFace : MonoBehaviour
    {
        public Collider collider;
        public int index;
        public MeshRenderer meshRenderer;


        [ColorUsageAttribute(true, true)]
        public Color poweredUpEmissionColor;
        [ColorUsageAttribute(true, true)]
        public Color poweredDownEmissionColor;

        public Texture defaultEmissiveTexture;
        public Texture defaultMainTexture;

        private Material _iconMaterial;
        private Material _backgroundMaterial;

        private void Start()
        {
            _backgroundMaterial = meshRenderer.materials[0];
            _iconMaterial = meshRenderer.materials[1];
        }

        public void Reset()
        {
            collider = GetComponentInChildren<Collider>();
        }

        public void SetDefaultTexture()
        {
            SetTexture(defaultMainTexture, defaultEmissiveTexture);
        }

        public void SetTexture(Texture main, Texture emissive)
        {
            Material[] materials = meshRenderer.materials;
            if (materials.Length > 1)
            {
                _iconMaterial.SetTexture("_MainTex", main);
                _iconMaterial.SetTexture("_EmissionMap", emissive);
                materials[1] = _iconMaterial;
                meshRenderer.materials = materials;
            }
        }

        public void PowerDown()
        {
            Material[] materials = meshRenderer.materials;
            if (materials.Length > 1)
            {
                materials[1].SetColor("_EmissionColor", poweredDownEmissionColor);
                meshRenderer.materials = materials;
            }
        }

        public void PowerUp()
        {
            Material[] materials = meshRenderer.materials;
            if (materials.Length > 1)
            {
                materials[1].SetColor("_EmissionColor", poweredUpEmissionColor);
                meshRenderer.materials = materials;
            }
        }

        public void SetActive(bool active)
        {
            if (active)
            {
                meshRenderer.materials = new Material[] { _backgroundMaterial, _iconMaterial };
                collider.enabled = true;
            }
            else
            {
                meshRenderer.materials = new Material[] { _backgroundMaterial };
                collider.enabled = false;
            }
        }
    }
}