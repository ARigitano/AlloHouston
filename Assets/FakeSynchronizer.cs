using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class FakeSynchronizer : MonoBehaviour
    {

        [SerializeField]
        private FakeTopScreen _fakeTopScreen;
        [SerializeField]
        private FakeTubeScreen _fakeTubeScreen;
        [SerializeField]
        private FakeTabletScreen _fakeTabletScreen;
        [SerializeField]
        private FakeBottomElement[] _fakeBottomElements;
        [SerializeField]
        private FakeHologram[] _fakeHolograms;
        [SerializeField]
        private FakeCornerScreen[] _fakeCornerScreens;
        [SerializeField]
        private FakeDoor _fakeDoor;

        private void Launched()
        {
            if(_fakeTopScreen != null) _fakeTopScreen.OnActivated();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnActivated();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnActivated();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if(fakeBottomElement != null) fakeBottomElement.OnActivated();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnActivated();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnActivated();

            if (_fakeDoor != null) _fakeDoor.OnActivated();
        }

        private void Paused()
        {
            if (_fakeTopScreen != null) _fakeTopScreen.OnPause();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnPause();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnPause();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if (fakeBottomElement != null) fakeBottomElement.OnPause();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnPause();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnPause();

            if (_fakeDoor != null) _fakeDoor.OnPause();
        }

        private void UnPaused()
        {
            if (_fakeTopScreen != null) _fakeTopScreen.OnUnpause();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnUnpause();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnUnpause();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if (fakeBottomElement != null) fakeBottomElement.OnUnpause();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnUnpause();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnUnpause();

            if (_fakeDoor != null) _fakeDoor.OnUnpause();
        }

        private void LaunchAction()
        {
            
        }

        private void Resolved()
        {
            if (_fakeTopScreen != null) _fakeTopScreen.OnResolved();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnResolved();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnResolved();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if (fakeBottomElement != null) fakeBottomElement.OnResolved();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnResolved();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnResolved();

            if (_fakeDoor != null) _fakeDoor.OnResolved();
        }

        private void Failed()
        {
            if (_fakeTopScreen != null) _fakeTopScreen.OnFailed();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnFailed();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnFailed();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if (fakeBottomElement != null) fakeBottomElement.OnFailed();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnFailed();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnFailed();

            if (_fakeDoor != null) _fakeDoor.OnFailed();
        }

        // Use this for initialization
        void Start()
        {
            Launched();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
