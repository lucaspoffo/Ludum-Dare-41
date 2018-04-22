using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour {

	public PathObject[] paths;
    public bool Loop = false;
    public int current = 0;
    public int dir = 1;
    public Transform target;

    private void Awake()
    {
        target.transform.position = paths[current].transform.position;
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, paths[current].transform.position);
        Debug.Log("Distance " + dist);
        if (dist < 1)
        {
            if(Loop)
            {
                if(current + 1 < paths.Length)
                {
                    current = current + 1;
                }
                else
                {
                    current = 0;
                }

            }
            else
            {
                if(dir == 1)
                {
                    if (current + 1 < paths.Length)
                    {
                        current += 1;
                    }
                    else
                    {
                        dir = -1;
                        current -= 1;
                    }
                } else
                {
                    if(current - 1 >= 0)
                    {
                        current -= 1;
                    } else
                    {
                        current += 1;
                        dir = 1;
                    }
                }
            }
           
            target.transform.position = paths[current].transform.position;
        }
        
    }
}

