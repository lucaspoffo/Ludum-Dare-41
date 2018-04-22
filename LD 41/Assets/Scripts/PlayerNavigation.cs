using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavigation : MonoBehaviour {

    public ObjectActions currentObject;
    Dictionary<string, ObjectActions> availableActions = new Dictionary<string, ObjectActions>();
    public GameController controller;

	// Use this for initialization
	void Awake () {
        controller = GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeObject(ObjectActions obj)
    {
        currentObject = obj;
        availableActions.Clear();
        for(int i = 0; i < obj.actions.Length; i++)
        {
            availableActions.Add(obj.actions[i].name, obj.actions[i]);
        }
        controller.changePlayerPosition();
    }

    public void tryAction(string action)
    {
        if(availableActions.ContainsKey(action))
        {
            changeObject(availableActions[action]);
            controller.DisplayCurrentObject();
        }
        else
        {
            Debug.Log("No object available!");
        }
    }
}
