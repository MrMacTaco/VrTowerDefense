using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] wayPoints; //Empty array to store waypoints
    private int nextWayPointIndex = 0; //Tracks next waypoint in series

    public int hp = 3; //Set enemy health
    public float speed = 10; //Set enemy speed

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        var finalWayPointIndex = wayPoints.Length - 1;
        Vector3 finalWayPoint = wayPoints[finalWayPointIndex].position + new Vector3(0,1,0);
        Vector3 nextWayPoint = wayPoints[nextWayPointIndex].position + new Vector3(0,1,0);
        Vector3 currentPosition = transform.position;
        Vector3 direction = nextWayPoint - currentPosition;

        //Enemy will move towards next waypoint, from last waypoint
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        //If enemy reachs new waypoint, update index
        if (Vector3.Distance(currentPosition, nextWayPoint) < 0.1f && nextWayPointIndex < finalWayPointIndex)
        {
            nextWayPointIndex += 1;
        }

        //If enemy reaches final waypoint, delete itself
        if (nextWayPointIndex == finalWayPointIndex && Vector3.Distance(currentPosition, finalWayPoint) < 0.1f)
        {
            Destroy(this.gameObject);
        }
        

    }

}
