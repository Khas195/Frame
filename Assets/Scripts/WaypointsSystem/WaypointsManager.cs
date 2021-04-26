using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : SingletonMonobehavior<WaypointsManager>
{
    [SerializeField]
    List<Waypoint> waypoints = new List<Waypoint>();
    // Start is called before the first frame update
    protected override void Awake()
    {
        waypoints.AddRange(this.GetComponentsInChildren<Waypoint>());
    }

    public Waypoint GetRandomWaypoint()
    {
        return waypoints[UnityEngine.Random.Range(0, waypoints.Count - 1)];
    }

}
