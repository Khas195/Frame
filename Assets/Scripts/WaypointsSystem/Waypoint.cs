using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    Waypoint previousWaypoint;
    [SerializeField]
    Waypoint nextWaypoint;
    [Range(0, 5)]
    public float width = 1.0f;

    public Vector3 GetPosition()
    {
        Vector3 maxBound = transform.position + transform.right * width / 2f;
        Vector3 minBound = transform.position - transform.right * width / 2f;
        var randomResult = Vector3.Lerp(minBound, maxBound, UnityEngine.Random.Range(0.0f, 1.0f));
        return randomResult;
    }

    public void SetPreviousWaypoint(Waypoint waypoint)
    {
        previousWaypoint = waypoint;
    }

    public Vector3 GetWalkableRightPoint()
    {
        return transform.position + transform.right * width / 2f;
    }

    public Waypoint GetPreviousWaypoint()
    {
        return previousWaypoint;
    }

    public Vector3 GetWalkableLeftPoint()
    {
        return transform.position - transform.right * width / 2f;
    }

    public Waypoint GetNextWaypoint()
    {
        return nextWaypoint;
    }

    public void SetNextWaypoint(Waypoint waypoint)
    {
        nextWaypoint = waypoint;
    }
}
