using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviors : MonoBehaviour
{
    private Transform waypoint; //Store the next waypoint enemy should travel to
    private int waypointIndex = 0; //Tracks next waypoint in series

    public int hp = 3; //Set enemy health
    public float speed = 10; //Set enemy speed

    private void Start()
    {
        waypoint = WaypointCollection.waypoints[waypointIndex]; //Initiates first waypoint location
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = waypoint.position - transform.position; //Sets direction between enemy position and next waypoint location
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, waypoint.position) <= 0.5) //Change next waypoint when enemy gets close to current waypoint
        {
            GetNextWaypoint();
        }
    }
    private void GetNextWaypoint()
    {    
        if (waypointIndex >= WaypointCollection.waypoints.Length - 1)
        {
            Destroy(this.gameObject);
            return;
        }
        waypointIndex += 1;
        waypoint = WaypointCollection.waypoints[waypointIndex];
    }

    private void OnTriggerEnter(Collider other)
    { 
        //Subtract health on bullet hit
        if(other.CompareTag("Bullet"))
        {
            hp -= 1;
            Destroy(other.gameObject);
        }
        
        //If no health remaining, destory enemy object
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
