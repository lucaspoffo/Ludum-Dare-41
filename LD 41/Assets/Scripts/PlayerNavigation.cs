using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerNavigation : MonoBehaviour {

    public ObjectActions currentObject;
    Dictionary<string, ObjectActions> availableActions = new Dictionary<string, ObjectActions>();
    public GameController controller;
    public Color activeColor;
    public Color inactiveColor;
    AIDestinationSetter destSetter;
    public GameObject player;
    public bool nearTarget;

	// Use this for initialization
	void Awake () {
        controller = GetComponent<GameController>();
        destSetter = player.GetComponent<AIDestinationSetter>();
        nearTarget = false;

    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, currentObject.transform.position);
        if (dist < 1)
        {
            nearTarget = true;
            for(int i = 0; i < currentObject.actions.Length; i++)
            {
                currentObject.actions[i].text.color = activeColor;
            }
        }
        else
        {
            nearTarget = false;
            for (int i = 0; i < currentObject.actions.Length; i++)
            {
                currentObject.actions[i].text.color = inactiveColor;
            }
        }
    }
    public void changeObject(ObjectActions obj)
    {
        currentObject = obj;

        foreach (KeyValuePair<string, ObjectActions> entry in availableActions)
        {
            // do something with entry.Value or entry.Key
            entry.Value.text.color = inactiveColor;
        }


        availableActions.Clear();
        for(int i = 0; i < obj.actions.Length; i++)
        {
            obj.actions[i].text.color = activeColor;
            availableActions.Add(obj.actions[i].name, obj.actions[i]);
        }
        //controller.changePlayerPosition();
        destSetter.target = currentObject.transform;
        nearTarget = false;
    }

    public void tryAction(string action)
    {
        if(availableActions.ContainsKey(action))
        {
            if(action == "exit")
            {
                controller.NextLevel();
            } else
            {
                changeObject(availableActions[action]);
            }
        }
        else
        {
            Debug.Log("No object available!");
        }
    }
}
