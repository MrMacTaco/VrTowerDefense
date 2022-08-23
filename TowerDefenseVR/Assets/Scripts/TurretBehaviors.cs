using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviors : MonoBehaviour
{

    [Header("Turret Stats")]
    public float range = 15.0f; //Stores the max firing range of turret
    public float rotateSpeed = 5.0f; //Stores rate of speed turret can rotate
    public float firerate = 2.0f; //Stores rate of turret fire in seconds
    public float damage = 1.0f; //Stores how much damage each shot does to enemy
    public float shotPower = 50f; //Affects how fast bullet travels

    [Header("Unity Values")]
    public string enemyTag = "Enemy"; //Stores whatever tag enemies are assigned in project
    public Transform pivot; //Stores location of pivot point on turret
    public GameObject bulletPrefab; //Store prefab bullet
    public Transform muzzleLocation; //Store coordinates of barrel location (where bullet should spawn)


    private Transform currentTarget; //Stores location of currently tracked enemy
    private GameObject currentTargetObject; //Stores the actual target GameObject
    private float fireCooldown = 0f; //Acts as timer which allows turret to shot at fireRate interval
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //Calls given function at an interval of twice per second
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            return;
        }

        //Calculate direction needed to face enemy, rotate turret cannon towards enemy direction
        Vector3 enemyDirection = currentTarget.position - transform.position;
        Quaternion quatRotation = Quaternion.LookRotation(enemyDirection);
        Vector3 eulerRotation = Quaternion.Lerp(pivot.rotation, quatRotation, Time.deltaTime * rotateSpeed).eulerAngles; //Converts quat angle to euler, and Lerp() smooths the rotation
        pivot.rotation = Quaternion.Euler(0f,eulerRotation.y,0f);

        //After turret rotates, fire shot if fireCooldown == 0
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / firerate;
        }

        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Find all enemies within level and place them in array
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        //Updates turret's target is suitable enemy is found
        if (closestEnemy != null && shortestDistance <= range)
        {
            currentTarget = closestEnemy.transform;
            currentTargetObject = closestEnemy;
        }
        else
        {
            currentTarget = null;
        }
    }

    //Fires projectile at targeted enemy
    void Shoot()
    {
       GameObject newBullet = (GameObject)Instantiate(bulletPrefab, muzzleLocation.position, muzzleLocation.rotation);
       TurretBullet newBulletObject = newBullet.GetComponent<TurretBullet>();

        if (newBulletObject != null)
        {
            newBulletObject.setUpBullet(currentTargetObject, damage, shotPower);
        }
    }

    //Draws a wirefame radius of turret range when selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
