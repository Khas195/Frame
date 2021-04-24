using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class AIController2D : MonoBehaviour
{
    [SerializeField]
    Waypoint currentWaypoint;
    [SerializeField]
    [ReadOnly]
    bool isGoingForward = true;

    [SerializeField]
    Character2D character;
    [SerializeField]
    GameObject aiEntity;
    [SerializeField]
    float stoppingDistance = 0.1f;

    [SerializeField]
    [ReadOnly]
    Vector3 targetPosition;
    [SerializeField]
    [ReadOnly]
    Vector2 currentDirection;
    private void Start()
    {
        targetPosition = currentWaypoint.GetPosition();
        currentDirection = (targetPosition - aiEntity.transform.position).normalized;
        isGoingForward = Random.Range(0.0f, 1.0f) >= 0.5f;
    }

    void Update()
    {
        if (HasReachedDestination() == false)
        {
            character.Move(currentDirection.x, currentDirection.y);
        }
        else
        {
            bool shouldBranch = false;
            if (currentWaypoint.GetBranches().Count > 0)
            {
                shouldBranch = Random.Range(0.0f, 1.0f) <= currentWaypoint.GetBranchRatio();
            }

            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.GetBranches()[Random.Range(0, currentWaypoint.GetBranches().Count - 1)];
            }
            else
            {
                ContinuePath();
            }

            targetPosition = currentWaypoint.GetPosition();
            currentDirection = (targetPosition - aiEntity.transform.position).normalized;
        }
    }

    private void ContinuePath()
    {
        if (isGoingForward)
        {
            if (currentWaypoint.GetNextWaypoint() == null)
            {
                isGoingForward = false;
                currentWaypoint = currentWaypoint.GetPreviousWaypoint();
            }
            else
            {
                currentWaypoint = currentWaypoint.GetNextWaypoint();
            }
        }
        else
        {
            if (currentWaypoint.GetPreviousWaypoint() == null)
            {
                isGoingForward = true;
                currentWaypoint = currentWaypoint.GetNextWaypoint();
            }
            else
            {
                currentWaypoint = currentWaypoint.GetPreviousWaypoint();
            }
        }
    }

    private bool HasReachedDestination()
    {
        return Vector2.Distance(aiEntity.transform.position, targetPosition) <= stoppingDistance;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(aiEntity.transform.position, targetPosition);
            Gizmos.DrawWireSphere(targetPosition, stoppingDistance);
        }
    }
}
