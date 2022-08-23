using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    [Header("Player Stats")]
    public int startMoney = 200;
    public int startLives = 10;

    public static int money;
    public static int lives;

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        lives = startLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
