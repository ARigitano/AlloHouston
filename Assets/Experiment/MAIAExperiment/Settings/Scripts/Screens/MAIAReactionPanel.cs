using CRI.HelloHouston.WindowTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAReactionPanel : Window
    {
        private bool _isTouched;
        private MAIATopScreen _topScreen;
        private MAIAManager _manager;
        public Texture selectedDiagram { get; set; }

        /// <summary>
        /// Tells the main screen that a reaction has been selected.
        /// </summary>
        public void SelectReaction()
        {
            if (!_isTouched)
            {
                _isTouched = true;
                if (selectedDiagram != null)
                {
                    bool correctDiagram = (selectedDiagram == _manager.selectedReaction.diagramImage);
                    _topScreen.OnReactionSelected(correctDiagram);
                }
                StartCoroutine(WaitButton());
            }
        }

        IEnumerator WaitButton()
        {
            yield return new WaitForSeconds(0.5f);
            _isTouched = false;
        }

        public void Init(MAIATopScreen topScreen, MAIAManager manager)
        {
            _topScreen = topScreen;
            _manager = manager;
        }
    }
}
