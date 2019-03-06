using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MaiaHologramTest : XPHologramElement
    {
        public bool isLoading = false;
        public List<ErrorTubeX> tubes;

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3f);
            LoadingFinished();
        }

        public void LoadingTube()
        {
            foreach(ErrorTubeX tube in tubes)
            {
                tube.IsNotAvailable();
            }
            StartCoroutine("Wait");
        }

        private void LoadingFinished()
        {
            foreach (ErrorTubeX tube in tubes)
            {
                tube.IsAvailable();
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
