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
        userInput = userInput.ToLower();
        playerNavigation.tryAction(userInput);
        InputComplete();
    }

    void InputComplete()
    {
        inputField.ActivateInputField();
        inputField.text = null;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
