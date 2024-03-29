using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PistolScript : MonoBehaviour
{
    [Header("Pistol Stats")]
    public float shotPower = 2500f; //Affects how fast bullet travels
    public float lifeSpan = 3f; //Determines how long bullet travels before deleting itself
    public float firerate = 2f; //Determines fire rate based in miliseconds

    [Header("Unity Values")]
    public List<XRController> controllers = null; //List that stores active XR Controllers
    public InputHelpers.Button trigger; //Stores which button should activate pistol
    public GameObject bulletPrefab; //Store prefab bullet
    public Transform muzzleLocation; //Store coordinates of barrel location (where bullet should spawn)

    private float fireCooldown = 0f;

    private void Start()
    {

    }
    void Update()
    {
        if (CanShoot() == true && fireCooldown <= 0)
        {
            Shoot();
            fireCooldown = 1f/firerate;
        }

        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }


    private void Shoot()
    {
        //Spawn a new bullet object at our set muzzle location
        var newBullet = Instantiate(bulletPrefab, muzzleLocation.position, muzzleLocation.transform.rotation);

        //Add the shotPower velocity to our new bullet
        newBullet.GetComponent<Rigidbody>().AddForce(muzzleLocation.forward * shotPower);

        //Rotate newBullet to match direction gun is pointing
        newBullet.transform.eulerAngles += new Vector3(0, 90, 90);

        //Destory the newBullet after lifeSpan seconds
        Destroy(newBullet, lifeSpan);
    }

    private bool CanShoot()
    {
        foreach (XRController controller in controllers)
        {
            InputHelpers.IsPressed(controller.inputDevice, trigger, out bool isPressed, 0.1f);
            if (isPressed == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
