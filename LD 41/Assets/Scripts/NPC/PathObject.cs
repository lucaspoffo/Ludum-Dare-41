using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObject : MonoBehaviour {

    public float waitTime;
    public PathAction[] actions ;
}

public enum actionType {WAIT, ROTATE}

[System.Serializable]
public class PathAction {
    public float duration;
    public float value;
    public actionType type;
    private float time;


    void Action()
    {
        
    }
}