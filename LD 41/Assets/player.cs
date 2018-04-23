using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class player : MonoBehaviour {

    Animator anim;
    AIPath ai;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ai = GetComponentInParent<AIPath>();
    }

    // Update is called once per frame
    void Update ()
    {
        anim.SetFloat("velocity", ai.velocity.magnitude);
	}
}
