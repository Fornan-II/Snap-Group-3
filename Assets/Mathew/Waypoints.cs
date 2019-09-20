using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //tells player how long to wait at the waypoint
    [SerializeField]
    public float waitSeconds = 0;

    //tells player how fast to go once leaving the waypoint
    [SerializeField]
    public float speedOut = 0;
}
