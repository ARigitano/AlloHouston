using CRI.HelloHouston;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Test
{
    public class CameraVisibleTest : CameraTarget
    {
        public override void OnVisibleExit(Camera camera)
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
        }

        public override void OnVisibleEnter(Camera camera)
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            gameObject.name += "+";
        }

        public override void OnVisibleStay(Camera camera)
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }
    }
}