using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    Rigidbody2D body = null;
    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    IMovement movement = null;


    // Start is called before the first frame update
    void Start()
    {
        movement.SetRigidBody(body);
    }

    public GameObject GetHost()
    {
        return body.gameObject;
    }

    public void Jump()
    {
        movement.SignalJump();
    }


    public void Move(float horizontal, float vertical)
    {
        movement.Move(vertical, horizontal);
    }

    public string GetName()
    {
        return name;
    }
}
