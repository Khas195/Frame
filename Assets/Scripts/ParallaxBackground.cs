using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[Serializable]

public class ParallaxObject
{
    [SerializeField]
    bool loop = false;
    [SerializeField]
    List<Transform> loopingTiles;
    public Transform objectTransform;
    float startPos;
    public float parallaxFactor;
    public void Init()
    {
        startPos = objectTransform.transform.position.x;

    }

    public void UpdatePosition(Vector3 cameraPosition)
    {
        float dist = (cameraPosition.x * parallaxFactor);
        this.objectTransform.position = new Vector3(startPos + dist, objectTransform.position.y, objectTransform.position.z);
    }
}
public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera = null;
    [SerializeField]
    List<ParallaxObject> parallaxObjects;

    void Start()
    {
        for (int i = 0; i < parallaxObjects.Count; i++)
        {
            parallaxObjects[i].Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    private void FixedUpdate()
    {
        if (mainCamera == null) return;
        for (int i = 0; i < parallaxObjects.Count; i++)
        {
            parallaxObjects[i].UpdatePosition(mainCamera.transform.position);
        }
    }
}
