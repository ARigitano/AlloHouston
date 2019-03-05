using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MaiaHologramTest : XPHologramElement
    {
        public bool isLoading = false;
        public List<GameObject> tubes;

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3f);
            isLoading = false;
        }

        public void LoadingTube()
        {
            isLoading = true;
            StartCoroutine("Wait");
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
