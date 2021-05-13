using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundRegister : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D cameraBound = null;
    // Start is called before the first frame update
    void Start()
    {
        ConstraintCamera.GetInstance().RegisterCameraBound(cameraBound);
    }

}
