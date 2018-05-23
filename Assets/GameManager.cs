// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int incidentsFixed = 0;
    [SerializeField]
    public TextMesh _tableText;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    /*public void StartCalibration() {

	}*/

    /*public void EndCalibration() {

	}*/

    /*public void StartGame() {

	}*/

    public void EndGame()
    {
        if (incidentsFixed == 3)
        {
            _tableText.text = "Victory";
            Debug.Log("End of game");
        }
    }
}
