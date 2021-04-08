using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Flip : MonoBehaviour
{
    [SerializeField]
    [Required]
    Rigidbody2D body = null;
    bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity.x < 0 && isFacingRight)
        {
            FlipModel();
        }
        else if (body.velocity.x > 0 && isFacingRight == false)
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
