using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollection : MonoBehaviour
{
    public static Transform[] waypoints;
    public static GameObject player;

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
