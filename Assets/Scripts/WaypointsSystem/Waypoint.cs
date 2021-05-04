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
    [SerializeField]
    List<Waypoint> branches = new List<Waypoint>();
    [SerializeField]
    [Range(0, 5)]
    float width = 1.0f;
    [SerializeField]
    [Range(0f, 1.0f)]
    float branchRatio = 0.5f;
    [SerializeField]
    private bool goForwardWhenEnterBranch = true;

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

    public float GetBranchRatio()
    {
        return branchRatio;
    }

    public Waypoint GetNextWaypoint()
    {
        return nextWaypoint;
    }

    public void SetNextWaypoint(Waypoint waypoint)
    {
        nextWaypoint = waypoint;
    }

    public List<Waypoint> GetBranches()
    {
        return branches;
    }

    public void AddBranch(Waypoint waypoint)
    {
        this.branches.Add(waypoint);
    }

    public bool ShouldGoForwardWhenEnteringBranch()
    {
        return goForwardWhenEnterBranch;
    }

    public float GetWidth()
    {
        return width;
    }

    public void SetRadius(float newWidth)
    {
        this.width = newWidth;
    }
}
