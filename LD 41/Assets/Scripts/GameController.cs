using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Pathfinding;

public class GameController : MonoBehaviour {

    public PlayerNavigation playerNavigation;
    public ObjectActions startObject;
    public GameObject player;
    public GameObject objectText;
    public List<ObjectActions> allObjects;
    public GameObject textPrefab;
    public Canvas canvas;
    public Color activeColor;
    public Color inactiveColor;

    public Text wasted;
    public Image redScreen;
    public Image blackScreen;

    private bool gameHasEnded = false;
    private bool passedLevel = false;
    public float restartDelay = 1f;
    public float loadNextLevelDelay = 2f;

    AIPath[] allMovingObjects;

    private void Awake()
    {
        allMovingObjects = Object.FindObjectsOfType<AIPath>();
        
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
        if(!gameHasEnded && !passedLevel)
        {
            gameHasEnded = true;
            GetComponent<AudioSource>().Play();
            Invoke("Restart", restartDelay);
            wasted.gameObject.SetActive(true);
            redScreen.gameObject.SetActive(true);

            Color fixedColor1 = redScreen.color;
            fixedColor1.a = 1;
            redScreen.color = fixedColor1;
            redScreen.CrossFadeAlpha(0f, 0f, true);
            redScreen.CrossFadeAlpha(0.6f, 1.0f, false);


            Color fixedColor2 = wasted.color;
            fixedColor2.a = 1;
            wasted.color = fixedColor2;
            wasted.CrossFadeAlpha(0f, 0f, true);
            wasted.CrossFadeAlpha(1, 1.0f, false);

            for (int i = 0; i < allMovingObjects.Length; i++)
            {
                allMovingObjects[i].maxSpeed = 0;

            }
        }
    }

	void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        if (!gameHasEnded && !passedLevel)
        {
            passedLevel = true;

            blackScreen.gameObject.SetActive(true);

            Color fixedColor1 = blackScreen.color;
            fixedColor1.a = 1;
            blackScreen.color = fixedColor1;
            blackScreen.CrossFadeAlpha(0f, 0f, true);
            blackScreen.CrossFadeAlpha(1f, 1.5f, false);


            for (int i = 0; i < allMovingObjects.Length; i++)
            {
                allMovingObjects[i].maxSpeed = 0;
            }
            Invoke("LoadNextLevel", loadNextLevelDelay);
        }
    }

    void LoadNextLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
