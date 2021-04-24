using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad()]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void onDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }
        Gizmos.DrawSphere(waypoint.transform.position, .1f);
        Gizmos.DrawLine(waypoint.GetWalkableLeftPoint(), waypoint.GetWalkableRightPoint());
        if (waypoint.GetPreviousWaypoint() != null)
        {
            Gizmos.color = Color.red;
            Vector3 from = waypoint.GetWalkableRightPoint();
            Vector3 to = waypoint.GetPreviousWaypoint().GetWalkableRightPoint();
            Gizmos.DrawLine(from, to);
        }
        if (waypoint.GetNextWaypoint() != null)
        {
            Gizmos.color = Color.green;
            Vector3 from = waypoint.GetWalkableLeftPoint();
            Vector3 to = waypoint.GetNextWaypoint().GetWalkableLeftPoint();
            Gizmos.DrawLine(from, to);
        }
        foreach (var branch in waypoint.GetBranches())
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
        }
    }
}
