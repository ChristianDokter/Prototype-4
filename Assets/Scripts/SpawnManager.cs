using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    //float voor enemyPrefab in Unity.
    public GameObject enemyPrefab;

    //float voor powerupPrefab in Unity.
    public GameObject powerupPrefab;

    //float voor spawnRange.
    private float spawnRange = 9.0f;

    //int voor de enemycount in Unity.
    public int enemyCount;

    //int voor waveNumber in Unity.
    public int waveNumber = 1;

    
    void Start()
    {
        SpawnEnemyWave(waveNumber);

        // Dit zorgt ervoor dat er een nieuwe powerup spawned
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

 
    void Update()
    {
        // Dit zorgt ervoor dat de computer weet wat enemycount is
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // Dit zorgt ervoor dat er een nieuwe wave komt als alle enemies dood xijn
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);

            // hierdoor spanwt er een nieuwe powerup
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    // Dit zorgt ervoor dat er enemies spawnen
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Dit zorgt ervoor dat de enemies spawnen op willekeureige plekken
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Dit maakt een nieuwe functie aan voor het spawnen van de enemies
    private Vector3 GenerateSpawnPosition()
    {
        // Dit maakt een spawn radius voor de enemy om te spawnen
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        // Dit zorgt ervoor dat het script opnieuw wordt uitgevoerd
        return randomPos;
    }
}
