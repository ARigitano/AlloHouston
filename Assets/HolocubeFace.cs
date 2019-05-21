using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.GameElements
{
    public class HolocubeFace : MonoBehaviour
    {
        public Collider face;
        public int index;
        public Sprite sprite;

        public void Reset()
        {
            face = GetComponentInChildren<Collider>();
            sprite = GetComponentInChildren<Sprite>();
        }
    }
}