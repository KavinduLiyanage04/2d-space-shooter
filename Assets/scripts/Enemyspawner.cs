/*
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;   // Assign your 2 (or more) enemy prefabs here
    public float spawnInterval = 2.5f;  // Time between spawns
    public float xRange = 8f;           // Horizontal spawn area
    public float spawnY = 4f;           // Spawn height (above screen)
    public GameObject Enemyboss;

    public bool stopSpawning = false;


    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval); // Start after 1 second, repeat every interval
    }

    void SpawnEnemy()
    {
        if (stopSpawning) return;

        if (enemyPrefabs.Length == 0) return;

        int index = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), spawnY, 0);
        Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
    }

    public void SpawnBoss()
    {
        // Despawn all normal enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemyship");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Spawn boss
        GameObject boss = Instantiate(Enemyboss, new Vector3(0, 8f, 0), Quaternion.identity);

        // Play intro sound
        /*
        AudioSource.PlayClipAtPoint(bossIntroClip, Camera.main.transform.position);
        */