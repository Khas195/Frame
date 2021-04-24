using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class AIController2D : MonoBehaviour
{
    [SerializeField]
    Waypoint currentWaypoint;
    [SerializeField]
    bool goForward = true;

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
    }

    void Update()
    {
        if (Vector2.Distance(aiEntity.transform.position, targetPosition) > stoppingDistance)
        {
            character.Move(currentDirection.x, currentDirection.y);
        }
        else
        {
            if (goForward)
            {
                currentWaypoint = currentWaypoint.GetNextWaypoint();
            }
            else
            {
                currentWaypoint = currentWaypoint.GetPreviousWaypoint();
            }
            targetPosition = currentWaypoint.GetPosition();
            currentDirection = (targetPosition - aiEntity.transform.position).normalized;
        }
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
