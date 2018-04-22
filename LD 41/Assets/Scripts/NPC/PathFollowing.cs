using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class PathFollowing : MonoBehaviour
{

    public PathObject[] paths;
    public bool Loop = false;
    public int current = 0;
    public int dir = 1;
    private int currentAction = 0;
    AIDestinationSetter destSetter;
    FieldOfView fieldOfView;
    public GameObject player;

    private void Awake()
    {
        
        destSetter = GetComponent<AIDestinationSetter>();
        fieldOfView = GetComponent<FieldOfView>();
        destSetter.target = paths[current].transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(updatePath());
    }

    private void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        Vector2 dir2 = new Vector2(dir.x, dir.y);
        Vector2 forwardDir = new Vector2(transform.up.x,transform.up.y);

        float angle = Vector2.Angle(forwardDir,dir2);

        if (angle < fieldOfView.viewAngle / 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dir2, fieldOfView.viewRadius);
            if (hit.collider != null)
            {
                if (hit.transform.tag == "Player")
                {
                    FindObjectOfType<GameController>().EndGame();
                }
            }
        }
    }

    private IEnumerator updatePath()
    {
        while (true)
        {

            float dist = Vector3.Distance(transform.position, paths[current].transform.position);
            if (dist < 1)
            {
                destSetter.target = null;
                for (int i = 0; i < paths[current].actions.Length; i++)
                {
                    if(paths[current].actions[i].type == actionType.ROTATE)
                    {
                        yield return StartCoroutine(doRotate(paths[current].actions[i]));
                    } 
                    else if(paths[current].actions[i].type == actionType.WAIT)
                    {
                        yield return new WaitForSeconds(paths[current].actions[i].duration);
                    }
                }
                if (paths[current].actions.Length > currentAction)
                {
                    
                }

                if (Loop)
                {
                    if (current + 1 < paths.Length)
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
                    if (dir == 1)
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
                    }
                    else
                    {
                        if (current - 1 >= 0)
                        {
                            current -= 1;
                        }
                        else
                        {
                            current += 1;
                            dir = 1;
                        }
                    }
                }

                destSetter.target = paths[current].transform;
            }
            yield return null;
        }
    }

    private IEnumerator doRotate(PathAction action)
    {
        float time = 0;
        float startAngle = transform.eulerAngles.z;

        while (time < action.duration)
        {
            time += Time.deltaTime;
            float angle = Mathf.LerpAngle(startAngle, action.value, time / action.duration);
            transform.eulerAngles = new Vector3(0, 0, angle);

            yield return null;
        }

    }
}

