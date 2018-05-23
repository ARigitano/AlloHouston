// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleButton : MonoBehaviour
{
    public GameObject _placeholder;
    [SerializeField] private Material _moduleMaterial;
    public bool isModule = false;
	[SerializeField] private int _index;
	private GameObject _room;
	private GameObject _mur;

	void Update() {
		_room = GameObject.FindGameObjectWithTag ("Room");
		if (_room != null) {

			_mur = GameObject.Find ("Mur" + _index);

			/*switch (_index) {
			case 0:
				
				break;
			case 1:

				break;
			case 2:

				break;
			default:

				break;
			}*/
		}
	}

    public void Activated()
    {
        _mur.GetComponent<MeshRenderer>().material = _moduleMaterial;
        //isModule = true;
        Debug.Log(_mur.name + " is now a module.");
    }
}
