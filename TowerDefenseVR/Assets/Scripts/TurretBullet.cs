using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private GameObject currentTarget; //Stores bullet's tracking target
    private float dmg; //Takes damage value of turret
    private float spd; //Takes bullet speed value of turret

    // Update is called once per frame
    void Update()
    {
        //Destroy bullet is target is lost
        if (currentTarget == null)
        {
            Destroy(this.gameObject);
            return;
        }

        //Calculate direction bullet should travel
        Vector3 direction = currentTarget.transform.position - transform.position;
        float bulletBuffer = spd * Time.deltaTime; //Calculates how far bullet will travel this frame

        //Determine if bullet will hit something if it moves, if so, a hit has occured, otherwise move to new location
        if (direction.magnitude <= bulletBuffer)
        {
            OnHit();
            return;
        }
        else
        {
            transform.Translate(direction.normalized * bulletBuffer, Space.World);
        }
    }

    //Takes attributes from turret and passes them onto new bullet objects when created
    public void setUpBullet (GameObject turretTarget, float damage, float speed)
    {
        currentTarget = turretTarget;
        dmg = damage;
        spd = speed;
    }

    //Deals damage to enemy whenever a hit is detected
    private void OnHit()
    {
        currentTarget.GetComponent<EnemyBehaviors>().hp -= dmg;
        Destroy(this.gameObject);
    }
}
