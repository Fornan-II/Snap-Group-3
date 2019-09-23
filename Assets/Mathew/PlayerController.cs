using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    Waypoints[] waypoints;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    public bool isCircular;

    [SerializeField]
    public bool inReverse = true;

    private Waypoints currentWaypoint;
    private int currentIndex = 0;
    private bool isWaiting = false;
    private float speedStorage = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(waypoints.Length > 0)
        {
            currentWaypoint = waypoints[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaypoint != null && !isWaiting)
        {
            MoveTowardsWaypoint();
        }
    }

    void Pause()
    {
        isWaiting = !isWaiting;
    }

    private void MoveTowardsWaypoint()
    {
        //get the objects curretn position
        Vector3 currentPosition = this.transform.position;

        //get the target waypoints position
        Vector3 targetPosition = currentWaypoint.transform.position;

        //if the object isn't that close to the waypoint
        if(Vector3.Distance(currentPosition, targetPosition) > .1f)
        {
            //get the direction and normalize
            Vector3 directionOfTravel = targetPosition - currentPosition;
            directionOfTravel.Normalize();

            //sclae the movement on each axis by the DirectonOfTravel vector
            this.transform.Translate(
                directionOfTravel.x * speed * Time.deltaTime,
                directionOfTravel.y * speed * Time.deltaTime,
                directionOfTravel.z * speed * Time.deltaTime,
                Space.World);
        }
        else
        {
            //if the waypoing has a pause amount then wait a bit
            if(currentWaypoint.waitSeconds > 0)
            {
                Pause();
                Invoke("Pause", currentWaypoint.waitSeconds);
            }

            //if the current waypoint has a speed change then change it to that speed
            if(currentWaypoint.speedOut > 0)
            {
                speedStorage = speed;
                speed = currentWaypoint.speedOut;
            }
            else if(speedStorage != 0)
            {
                speed = speedStorage;
                speedStorage = 0;
            }
            NextWaypoint();
        }
    }

    private void NextWaypoint()
    {
        if (isCircular)
        {
            if (!inReverse)
            {
                currentIndex = (currentIndex + 1 >= waypoints.Length) ? 0 : currentIndex + 1;
            }
            else
            {
                currentIndex = (currentIndex == 0) ? waypoints.Length - 1 : currentIndex - 1;
            }
        }

        else
        {
            if((!inReverse && currentIndex + 1 >= waypoints.Length) || (inReverse && currentIndex == 0))
            {
                inReverse = !inReverse;
            }

            currentIndex = (!inReverse) ? currentIndex + 1 : currentIndex - 1;
        }
        currentWaypoint = waypoints[currentIndex];
    }
}
