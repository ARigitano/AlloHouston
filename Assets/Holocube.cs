using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.GameElements
{
    public class Holocube : MonoBehaviour
    {
        public HolocubeFace stationFace;
        public HolocubeFace tubexFace;
        public HolocubeFace xpLeftFace;
        public HolocubeFace xpRightFace;

        public HolocubeFace[] faces
        {
            get
            {
                return new HolocubeFace[] { stationFace, tubexFace, xpLeftFace, xpRightFace };
            }
        }

        private bool _firstActivation;

        public void PowerDown()
        {
            stationFace.face.enabled = false;
            tubexFace.face.enabled = false;
            xpLeftFace.face.enabled = false;
            xpRightFace.face.enabled = false;
        }
        
        public void PowerUp()
        {
            if (_firstActivation)
                ActivatedState();
            else
                StartingState();
        }

        public void StartingState()
        {
            stationFace.face.enabled = true;
            tubexFace.face.enabled = false;
            xpLeftFace.face.enabled = false;
            xpRightFace.face.enabled = false;
        }

        public void ActivatedState()
        {
            _firstActivation = true;
            stationFace.face.enabled = true;
            tubexFace.face.enabled = true;
            xpLeftFace.face.enabled = true;
            xpRightFace.face.enabled = true;
        }
    }
}
