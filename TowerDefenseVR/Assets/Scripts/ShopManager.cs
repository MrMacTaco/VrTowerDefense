using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //Upgrade pistol if player has enough money
    public void UpgradePistolFirerate(PistolScript pistol)
    {
        if (StatsManager.money >= 150)
        {
            pistol.firerate += 1;
            StatsManager.money -= 150;
        }
    }
    //Upgrade pistol if player has enough money
    public void UpgradePistolVelocity(PistolScript pistol)
    {
        if (StatsManager.money >= 150)
        {
            pistol.shotPower += 100;
            StatsManager.money -= 200;
        }
    }
    //Upgrade player's walk speed if they have enough money
    public void UpgradePlayerSpeed(PlayerMovement player)
    {
        if (StatsManager.money >= 100)
        {
            player.speed += 1;
            StatsManager.money -= 100;
        }
    }
}
