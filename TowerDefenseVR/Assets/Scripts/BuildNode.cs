using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNode : MonoBehaviour
{
    public GameObject basicTower; //Store basic tower prefeb

    public void buildBasic()
    {
        if (StatsManager.money >= 200)
        {
            Instantiate(basicTower, this.transform.position, this.transform.rotation);
            StatsManager.money -= 200;
            Destroy(this.gameObject);
        }
    }
}
