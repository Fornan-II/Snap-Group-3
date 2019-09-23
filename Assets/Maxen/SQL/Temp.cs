using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class Temp : MonoBehaviour
{
    public PlayerController player;

    private void Start()
    {
        if(player)
        {
            player.OnWaypointReached += LogWaypointReached;
        }
    }

    private void LogWaypointReached()
    {
        Debug.Log("Waypoint reached");
    }
}