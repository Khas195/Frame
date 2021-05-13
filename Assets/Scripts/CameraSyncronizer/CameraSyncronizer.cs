using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSyncronizer : MonoBehaviour
{
    [SerializeField]
    CameraFollow follow;
    [SerializeField]
    ConstraintCamera constraint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (follow.enabled)
        {
            follow.Follow();
        }
        else
        {

            constraint.UpdateCameraCapturingBound();
        }
        constraint.UpdateCameraMapBound();
    }
}
