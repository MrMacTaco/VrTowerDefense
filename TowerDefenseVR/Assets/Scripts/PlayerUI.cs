using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //Simple Script that updates player UI
    public Text livesText;
    public Text moneyText;

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + StatsManager.lives.ToString();
        moneyText.text = "Money: " + StatsManager.money.ToString();
    }
}
