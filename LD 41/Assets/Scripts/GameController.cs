using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public PlayerNavigation playerNavigation;
    public ObjectActions startObject;
    public GameObject player;

	// Use this for initialization
	void Start () {
        playerNavigation = GetComponent<PlayerNavigation>();
        playerNavigation.changeObject(startObject);
        DisplayCurrentObject();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changePlayerPosition()
    {
        player.transform.position = playerNavigation.currentObject.transform.position;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 1);
    }

    public void DisplayCurrentObject()
    {
        Debug.Log(playerNavigation.currentObject.name);
        Debug.Log("Available Objects: ");
        for(int i = 0; i < playerNavigation.currentObject.actions.Length;i++)
        {
            Debug.Log(playerNavigation.currentObject.actions[i].name);
        }
        Debug.Log("-----------------");
    }
}
