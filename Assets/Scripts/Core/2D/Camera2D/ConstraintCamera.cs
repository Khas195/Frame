﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintCamera : SingletonMonobehavior<ConstraintCamera>
{
    [SerializeField]
    Transform host = null;
    [SerializeField]
    BoxCollider2D boundary = null;
    [SerializeField]
    Camera cam = null;

    public void RegisterCameraBound(BoxCollider2D cameraBound)
    {
        this.boundary = cameraBound;
    }

    public bool isAtBound = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateCamera()
    {
        if (this.boundary == null)
        {
            LogHelper.LogError("Camera Bound is missing the Boundary Box!!.", true);
            return;
        }

        var hostPos = host.position;

        var cameraSizeY = 2 * cam.orthographicSize;
        var cameraSizeX = cameraSizeY * cam.aspect;
        hostPos = Constraint(hostPos, cameraSizeY, cameraSizeX);

        host.transform.position = hostPos;
    }

    private Vector3 Constraint(Vector3 hostPos, float cameraSizeY, float cameraSizeX)
    {
        isAtBound = false;
        var rightBound = boundary.transform.position.x + boundary.bounds.extents.x;
        var leftBound = boundary.transform.position.x - boundary.bounds.extents.x;
        if (hostPos.x + cameraSizeX / 2 >= rightBound)
        {
            hostPos.x = rightBound - cameraSizeX / 2;
            isAtBound = true;
        }
        else if (hostPos.x - cameraSizeX / 2 <= leftBound)
        {
            hostPos.x = leftBound + cameraSizeX / 2;
            isAtBound = true;
        }

        var upperBound = boundary.transform.position.y + boundary.bounds.extents.y;
        var lowerBound = boundary.transform.position.y - boundary.bounds.extents.y;
        if (hostPos.y + cameraSizeY / 2 >= upperBound)
        {
            hostPos.y = upperBound - cameraSizeY / 2;
            isAtBound = true;
        }
        else if (hostPos.y - cameraSizeY / 2 <= lowerBound)
        {
            hostPos.y = lowerBound + cameraSizeY / 2;
            isAtBound = true;
        }

        return hostPos;
    }
}
