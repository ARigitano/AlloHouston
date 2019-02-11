using CRI.HelloHouston;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Test
{
    public class CameraVisibleTest : MonoBehaviour, ICameraTarget
    {
        public void OnVisibleExit()
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
        }

        public void OnVisibleEnter()
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            gameObject.name += "+";
        }

        public void OnVisibleStay()
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }
    }
}