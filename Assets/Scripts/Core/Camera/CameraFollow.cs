using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform host = null;
    [SerializeField]
    List<Transform> encapsolatedTarget = new List<Transform>();
    [SerializeField]
    Transform character = null;

    [SerializeField]
    [Tooltip("Each frame the camera move x percentage closer to the target")]
    [Range(0.0f, 1.0f)]
    float followPercentage = 0.02f;
    [SerializeField]
    bool followX = false;
    [SerializeField]
    bool followY = false;
    [SerializeField]
    bool followZ = false;
    [SerializeField]
    float maxDistance = 5f;


    // Start is called before the first frame update
    void Start()
    {
        encapsolatedTarget.Add(character);
    }

    void FixedUpdate()
    {
        var targetPos = GetCenterPosition(encapsolatedTarget);
        var hostPos = host.position;
        if (followX)
        {
            hostPos.x = Mathf.Lerp(hostPos.x, targetPos.x, followPercentage);
        }
        if (followY)
        {
            hostPos.y = Mathf.Lerp(hostPos.y, targetPos.y, followPercentage);
        }
        if (followZ)
        {
            hostPos.z = Mathf.Lerp(hostPos.z, targetPos.z, followPercentage);
        }
        host.transform.position = hostPos;
    }


    public void SetFollowPercentage(float value)
    {
        followPercentage = value;
    }

    public void Clear(bool clearPlayer)
    {
        encapsolatedTarget.Clear();
        if (clearPlayer == false)
        {
            encapsolatedTarget.Add(character);
        }
    }

    public float GetFollowPercentage()
    {
        return followPercentage;
    }

    public void AddEncapsolateObject(Transform obj)
    {
        this.encapsolatedTarget.Add(obj);
    }

    private Vector3 GetCenterPosition(List<Transform> listOfTargets)
    {
        var bounds = new Bounds(listOfTargets[0].position, Vector3.zero);
        foreach (var target in listOfTargets)
        {
            bounds.Encapsulate(target.position);
        }
        return bounds.center;
    }

}
