using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintCamera : MonoBehaviour
{
    [SerializeField]
    Transform host = null;
    [SerializeField]
    BoxCollider2D boundary = null;
    [SerializeField]
    Camera cam = null;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void LateUpdate()
    {
        var hostPos = host.position;

        var cameraSizeY = 2 * cam.orthographicSize;
        var cameraSizeX = cameraSizeY * cam.aspect;
        hostPos = Constraint(hostPos, cameraSizeY, cameraSizeX);

        host.transform.position = hostPos;

    }
   private Vector3 Constraint(Vector3 hostPos, float cameraSizeY, float cameraSizeX)
    {
        var rightBound = boundary.transform.position.x + boundary.bounds.extents.x;
        var leftBound = boundary.transform.position.x - boundary.bounds.extents.x;
        if (hostPos.x + cameraSizeX / 2 >= rightBound)
        {
            hostPos.x = rightBound - cameraSizeX / 2;
        }
        else if (hostPos.x - cameraSizeX / 2 <= leftBound)
        {
            hostPos.x = leftBound + cameraSizeX / 2;
        }

        var upperBound = boundary.transform.position.y + boundary.bounds.extents.y;
        var lowerBound = boundary.transform.position.y - boundary.bounds.extents.y;
        if (hostPos.y + cameraSizeY / 2 >= upperBound)
        {
            hostPos.y = upperBound - cameraSizeY / 2;
        }
        else if (hostPos.y - cameraSizeY / 2 <= lowerBound)
        {
            hostPos.y = lowerBound + cameraSizeY / 2;
        }

        return hostPos;
    }
}
