using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointManagerWindow>();
    }
    [SerializeField]
    Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));
        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }
        obj.ApplyModifiedProperties();
    }

    private void DrawButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if (GUILayout.Button("Add Previous Waypoint"))
            {
                AddPreviousWaypoint();
            }
            if (GUILayout.Button("Add Next Waypoint"))
            {
                AddNextWaypoint();
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }
    }

    private void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.parent = waypointRoot.transform;
        var waypoint = waypointObject.GetComponent<Waypoint>();
        if (waypointRoot.childCount > 1)
        {
            var previousWayPoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponentInParent<Waypoint>();
            waypoint.SetPreviousWaypoint(previousWayPoint);
            previousWayPoint.SetNextWaypoint(waypoint);

            waypoint.transform.position = previousWayPoint.transform.position;
            waypoint.transform.up = previousWayPoint.transform.up;
        }
        Selection.activeGameObject = waypoint.gameObject;
    }
    void AddPreviousWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.parent = waypointRoot.transform;
        var newWaypoint = waypointObject.GetComponent<Waypoint>();

        var selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.right = selectedWaypoint.transform.right;

        if (selectedWaypoint.GetPreviousWaypoint() != null)
        {
            var previousWaypoint = selectedWaypoint.GetPreviousWaypoint();
            previousWaypoint.SetNextWaypoint(newWaypoint);
            newWaypoint.SetPreviousWaypoint(previousWaypoint);
        }
        newWaypoint.SetNextWaypoint(selectedWaypoint);
        selectedWaypoint.SetPreviousWaypoint(newWaypoint);
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }
    void AddNextWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.parent = waypointRoot.transform;
        var newWaypoint = waypointObject.GetComponent<Waypoint>();

        var selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.right = selectedWaypoint.transform.right;

        if (selectedWaypoint.GetNextWaypoint() != null)
        {
            var nextWaypoint = selectedWaypoint.GetNextWaypoint();
            nextWaypoint.SetPreviousWaypoint(newWaypoint);
            newWaypoint.SetNextWaypoint(nextWaypoint);
        }
        newWaypoint.SetPreviousWaypoint(selectedWaypoint);
        selectedWaypoint.SetNextWaypoint(newWaypoint);
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex() + 1);
        Selection.activeGameObject = newWaypoint.gameObject;

    }
    void RemoveWaypoint()
    {
        var selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        var nextWaypoint = selectedWaypoint.GetNextWaypoint();
        var previousWaypoint = selectedWaypoint.GetPreviousWaypoint();
        if (nextWaypoint)
        {
            nextWaypoint.SetPreviousWaypoint(previousWaypoint);
            Selection.activeGameObject = nextWaypoint.gameObject;
        }
        if (previousWaypoint)
        {
            previousWaypoint.SetNextWaypoint(nextWaypoint);
            Selection.activeGameObject = previousWaypoint.gameObject;
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }
}
