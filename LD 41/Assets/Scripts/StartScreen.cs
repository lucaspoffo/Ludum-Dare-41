using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

    public InputField inputField;
    public Text start;
    public Color readyColor;

    // Use this for initialization
    void Awake()
    {
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    private void Start()
    {
        inputField.ActivateInputField();
    }

    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToUpper();
        if(userInput == "START")
        {
            start.color = readyColor;
            NextLevel();
        }
        inputField.text = null;
        inputField.ActivateInputField();
    }

    public void NextLevel()
    {
        Invoke("LoadNextLevel", 1f);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
