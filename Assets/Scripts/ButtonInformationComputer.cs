using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInformationComputer : MonoBehaviour {

    [SerializeField] private InformationsComputer _computer;
    [SerializeField] private bool _isIncrease; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="ViveController")
        {
            if (_computer._imageIndex >= _computer._diagramms.Length || _computer._imageIndex <= _computer._diagramms.Length)
                _computer._imageIndex = 0;
            else if (_isIncrease)
                _computer._imageIndex++;
            else if (!_isIncrease)
                _computer._imageIndex--;

        }
    }

}
