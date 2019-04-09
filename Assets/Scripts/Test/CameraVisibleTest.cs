using CRI.HelloHouston;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Test
{
    public class CameraVisibleTest : MonoBehaviour
    {
        public void OnVisibleExit(Camera camera)
        {
            Debug.Log(string.Format("Exit {0}", camera));
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
            gameObject.name += "-";
        }

        public void OnVisibleEnter(Camera camera)
        {
            Debug.Log(string.Format("Enter {0}", camera));
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            gameObject.name += "+";
        }

        public void OnVisibleStay(Camera camera)
        {
            Debug.Log(string.Format("Stay {0}", camera));
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }
    }
}