using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButon : MonoBehaviour {

	[SerializeField] private ColorXP _colorXP;
	private int _colorCounter = 0;
	private int _buttonSign; //1 = plus, 2 = minus

	void OnTriggerEnter(Collider other) {
		if (other.tag == "ViveController") {
			_colorXP.changeScreenColor (_colorCounter);

			if (_buttonSign == 1) {
				if (_colorCounter < _colorXP._screenMaterials.Length) 
						_colorCounter++;
				else
					_colorCounter = 0;
			} else if (_buttonSign == 2) {
				if (_colorCounter > 0) 
					_colorCounter--;
				else
					_colorCounter = _colorXP._screenMaterials.Length;
			} else {
				Debug.Log ("_buttonSign entered wrongly.");
			}
		}
	}
}
