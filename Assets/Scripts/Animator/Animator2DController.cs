using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator2DController : MonoBehaviour
{
    [SerializeField]
    Animator characterAnim = null;
    [SerializeField]
    Rigidbody2D characterBody = null;
    // Update is called once per frame
    void Update()
    {
        characterAnim.SetFloat("Speed", characterBody.simulated == true ? characterBody.velocity.magnitude : 0);
    }
}
