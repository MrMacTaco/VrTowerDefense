using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviors : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float hp; //Set enemy health
    public float speed = 10; //Set enemy speed
    public int lifePenality = 1; //Determines how many lives player loses if enemy reaches end
    public int value = 10; //Set value pl;ayer recieves when destroying enemy

    [Header("Unity Stuff")]
    public Canvas healthBarCanvas;
    public Image healthBar;

    private Transform waypoint; //Store the next waypoint enemy should travel to
    private GameObject player; //Stores player's location
    private int waypointIndex = 0; //Tracks next waypoint in series
    private float maxHP = 3;

    private void Start()
    {
        waypoint = WaypointCollection.waypoints[waypointIndex]; //Initiates first waypoint location
        player = WaypointCollection.player;
        hp = maxHP;
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

        healthBar.fillAmount = hp / maxHP; //Updates health bar
        healthBarCanvas.transform.LookAt(player.transform); //Keeps health bar facing player

        if (hp <= 0)
        {
            OnKill();
        }
    }
    private void GetNextWaypoint()
    {   
        //Condition for when final waypoint is reached
        if (waypointIndex >= WaypointCollection.waypoints.Length - 1)
        {

            EndReached();
            return;
        }
        waypointIndex += 1;
        waypoint = WaypointCollection.waypoints[waypointIndex];
    }

    //Old function for taking damage, still works with pistol, keep this until I rework pistol
    private void OnTriggerEnter(Collider other)
    { 
        //Subtract health on bullet hit
        if(other.CompareTag("Bullet"))
        {
            hp -= 1;
            Destroy(other.gameObject);
        }
    }

    //If enemy reachs end of track, delete them and subtract player lives
    private void EndReached()
    {
        StatsManager.lives -= lifePenality;
        Destroy(this.gameObject);
    }

    //If enemy is killed, give player money and delete enemy object
    private void OnKill()
    {
        StatsManager.money += value;
        Destroy(this.gameObject);
    }
}
