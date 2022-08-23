using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab; //Stores basic enemy prefab
    public Transform bossPrefab; //Stores basic enemy prefab

    public Transform spawnLocation; //Store the starting waypoint's position

    public float waveTimer = 5.0f; //Stores time gap between waves
    private float enemyGapTime = 0.5f; //Stores amount of time in seconds that Unity should wait to spawn next enemy in wave
    private float countdownTimer = 2.0f; //Acts as a timer for starting the next wave

    public static int waveNumber = 0; //Tracks which wave player is on

    int[] waveValue = new int[10] { 10, 15, 20, 30, 30, 35, 35, 40, 45, 5 };

    /* Wave spawn no longer just time based, so this update function is depricated
    void Update()
    {
        if (countdownTimer <= 0)
        {
            StartCoroutine(StartWave());
            countdownTimer = waveTimer;
        }
        countdownTimer -= Time.deltaTime;
    }*/

    /// <summary>
    /// IEnumerator allows use to 'pause' our function using a coroutine. So this function will spawn an enemy when called,
    /// then wait for specified time before spawning next enemy. Wave will be completely spawned before coroutine ends and
    /// allows Update() to continue executing
    /// </summary>
    IEnumerator StartWave()
    {
        if (waveNumber > 10)
        {
            yield return new WaitForSeconds(enemyGapTime);
        }
        waveNumber += 1;

        if (waveNumber == 10)
        {
            Instantiate(bossPrefab, spawnLocation.position, spawnLocation.rotation);
            yield return new WaitForSeconds(enemyGapTime);
        }

        for (int i = 0; i < waveValue[waveNumber-1]; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(enemyGapTime);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnLocation.position, spawnLocation.rotation);
    }

    public void ButtonStartWave()
    {
        StartCoroutine(StartWave());
    }
}
