using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    //Upgrade tower damage
    public void UpgradeTowerDamage(TurretBehaviors tower)
    {
        if (StatsManager.money >= 100)
        {
            tower.damage += 1;
            StatsManager.money -= 100;
        }
    }

    //Upgrade tower firerate
    public void UpgradeTowerSpeed(TurretBehaviors tower)
    {
        if (StatsManager.money >= 125)
        {
            tower.firerate += 1;
            StatsManager.money -= 125;
        }
    }
}
