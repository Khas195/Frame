using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
    [SerializeField]
    List<Waypoint> waypoints = new List<Waypoint>();
    [SerializeField]
    List<AIController2D> citizens = new List<AIController2D>();
    [SerializeField]
    Transform citizenRoot = null;
    // Start is called before the first frame update
    protected void Awake()
    {
        waypoints.AddRange(this.GetComponentsInChildren<Waypoint>());
        if (citizenRoot != null)
        {
            citizens.AddRange(citizenRoot.GetComponentsInChildren<AIController2D>());
        }
    }
    private void Start()
    {
        citizens.ForEach((AIController2D curCitizen) =>
        {
            curCitizen.SetWaypoint(this.GetRandomWaypoint());
        });
    }

    public Waypoint GetRandomWaypoint()
    {
        return waypoints[UnityEngine.Random.Range(0, waypoints.Count - 1)];
    }

}
