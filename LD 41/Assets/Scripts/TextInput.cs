using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

    public InputField inputField;
    public PlayerNavigation playerNavigation;

	// Use this for initialization
	void Awake ()
    {
        playerNavigation = GetComponent<PlayerNavigation>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
        inputField.ActivateInputField();
    }
	
    void AcceptStringInput(string userInput)
    {
        if(playerNavigation.nearTarget)
        {
            userInput = userInput.ToLower();
            playerNavigation.tryAction(userInput);
            inputField.text = null;
        }
        inputField.ActivateInputField();
    }

	// Update is called once per frame
	void Update () {
		if(playerNavigation.nearTarget)
        {
            inputField.enabled = true;
            inputField.ActivateInputField();
        }
        else
        {
            inputField.enabled = false;
        }
	}
}
