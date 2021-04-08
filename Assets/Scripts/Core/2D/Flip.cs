using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Flip : MonoBehaviour
{
    [SerializeField]
    bool useRigidBody = false;
    [SerializeField]
    [Required]
    [ShowIf("useRigidBody")]
    Rigidbody2D body = null;

    [SerializeField]
    bool useMovementScript = false;
    [SerializeField]
    [Required]
    [ShowIf("useMovementScript")]
    IMovement movementBehavior = null;
    bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velToAsset = Vector3.zero;
        if (useRigidBody)
        {
            velToAsset = body.velocity;
        }
        else if (useMovementScript)
        {
            velToAsset = movementBehavior.GetVelocity();
        }
        if (velToAsset.x < 0 && isFacingRight)
        {
            FlipModel();
        }
        else if (velToAsset.x > 0 && isFacingRight == false)
        {
            FlipModel();
        }
    }

    private void FlipModel()
    {
        isFacingRight = !isFacingRight;
        var localScale = body.transform.localScale;
        localScale.x *= -1;
        body.transform.localScale = localScale;
    }
}
