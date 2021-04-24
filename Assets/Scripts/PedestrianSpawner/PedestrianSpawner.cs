using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    [SerializeField]
    WaypointsManager manager;
    [SerializeField]
    GameObject citizenPrefab = null;
    [SerializeField]
    int spawnAmount = 5;
    void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            var newCitizen = GameObject.Instantiate(citizenPrefab, this.transform);
            var waypoint = manager.GetRandomWaypoint();
            var controller = newCitizen.GetComponentInChildren<AIController2D>();
            controller.SetWaypoint(waypoint);
            newCitizen.transform.position = waypoint.GetPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
