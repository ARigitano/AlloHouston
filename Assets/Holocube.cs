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
            foreach (var face in faces)
                face.PowerDown();
            stationFace.collider.enabled = false;
            tubexFace.collider.enabled = false;
            xpLeftFace.collider.enabled = false;
            xpRightFace.collider.enabled = false;
        }
        
        public void PowerUp()
        {
            foreach (var face in faces)
                face.PowerUp();
            if (_firstActivation)
                ActivatedState();
            else
                StartingState();
        }

        public void StartingState()
        {
            stationFace.SetActive(true);
            tubexFace.SetActive(false);
            xpLeftFace.SetActive(false);
            xpRightFace.SetActive(false);
        }

        public void ActivatedState()
        {
            _firstActivation = true;
            stationFace.SetActive(true);
            tubexFace.SetActive(true);
        }
    }
}
