using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
    [SerializeField]
    List<Waypoint> waypoints = new List<Waypoint>();
    [SerializeField]
    float waypointVisualSize = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDrawGizmos()
    {
        if (waypoints.Count > 0)
        {
            Gizmos.DrawWireSphere(waypoints[0].GetPosition(), waypointVisualSize);
            for (int i = 1; i < waypoints.Count; i++)
            {
                Gizmos.DrawWireSphere(waypoints[i].GetPosition(), waypointVisualSize);
            }
        }

    }
}
