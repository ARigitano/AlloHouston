using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLanguage : MonoBehaviour {

    //private Translation translation;
    [SerializeField]
    private Language languageManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeLanguage(string langue)
    {

        GameObject[] languages = GameObject.FindGameObjectsWithTag("Language");
        foreach(GameObject language in languages)
        {
            if (language.GetComponent<Translation>().language == langue)
            {
                languageManager.translation = language.GetComponent<Translation>();
                //Debug.Log(translation);
            }
        }

        /*GameObject[] traductions = GameObject.FindGameObjectsWithTag("Translation");

        foreach (GameObject traduction in traductions)
        {
            traduction.GetComponent<TranslationPanel>().translation = translation;
        }*/
    }
}
