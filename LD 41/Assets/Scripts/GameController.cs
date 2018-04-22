using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public PlayerNavigation playerNavigation;
    public ObjectActions startObject;
    public GameObject player;
    public GameObject objectText;
    public List<ObjectActions> allObjects;
    public GameObject textPrefab;
    public Camera camera;
    public Canvas canvas;
    public Color activeColor;
    public Color inactiveColor;

    private bool gameHasEnded = false;
    public float restartDelay = 1f;
    public float loadNextLevelDelay = 1f;

    private void Awake()
    {
        ObjectActions[] all = Object.FindObjectsOfType<ObjectActions>();
        for(int i = 0; i < all.Length; i++)
        {
            allObjects.Add(all[i]);
            GameObject textObject = Instantiate(textPrefab);
            textObject.transform.SetParent(canvas.transform, false);
            RectTransform rect = textObject.GetComponent<RectTransform>();
            Text text = textObject.GetComponent<Text>();
            text.text = all[i].name;
            all[i].text = text;
            all[i].text.color = inactiveColor;
            all[i].textTransform = rect;
            //rect.position = camera.WorldToScreenPoint(all[i].transform.position);
            rect.position = all[i].transform.position;
        }


    }

    // Use this for initialization
    void Start () {
        playerNavigation = GetComponent<PlayerNavigation>();
        playerNavigation.changeObject(startObject);
    }

    public void EndGame()
    {
        if(!gameHasEnded)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

	void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Invoke("LoadNextLevel", loadNextLevelDelay);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
