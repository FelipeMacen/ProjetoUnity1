using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance {get; private set;}

    [Header("Referências")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Configurações das ondas")]
    [SerializeField] private int startingEnemies = 5;
    [SerializeField] private float initialEnemySpeed = 3.5f;
    [SerializeField] private float speedIncreaseWave = 0.25f;
    [SerializeField] private float timeBetweenWaves = 0.3f;

    private int currentWave;
    private int enemiesRemaining;
    private bool waitingForNextWave;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        if (enemyPrefab == null ||
        player == null ||
        spawnPoints == null ||
        spawnPoints.Length == 0)
        {
            Debug.LogError("configurações faltando.");
            
            enabled = false;
            return;
        }

        if (enemyPrefab.GetComponent<EnemyController>() == null)
        {
            Debug.LogError("configurações faltando (PREFAB).");
            
            enabled = false;
            return;
        }

        StartWave();
    }

    private void StartWave()
    {
        currentWave++;

        int enemiesToSpawn = startingEnemies + currentWave - 1;

        enemiesRemaining = enemiesToSpawn;

        float waveSpeed = initialEnemySpeed + (currentWave - 1) * speedIncreaseWave;

        //acho que vou remover wave text. não eh minha ideia te lo

        List<Transform> avaiableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (avaiableSpawnPoints.Count == 0)
            {
                avaiableSpawnPoints.AddRange(spawnPoints);
            }

            int index = Random.Range(0, avaiableSpawnPoints.Count);

            avaiableSpawnPoints[index].position += player.position; 

            Transform spawnPoint = avaiableSpawnPoints[index];

            avaiableSpawnPoints[index].position -= player.position;

            avaiableSpawnPoints.RemoveAt(index);

            GameObject enemyObject = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyController enemy = enemyObject.GetComponent<EnemyController>();

            enemy.Initialize(player, waveSpeed);
        }

        Debug.Log($"Onda {currentWave}: {enemiesToSpawn} inimigos");
    }

    public void EnemyDefeated()
    {
        if (enemiesRemaining <= 0)
        {
            return;
        }

        enemiesRemaining--;

        Debug.Log($"Inimigos Restantes: {enemiesRemaining}");

        if (enemiesRemaining == 0 && !waitingForNextWave)
        {
            StartCoroutine(BeginNextWave());
        }
    }

    private IEnumerator BeginNextWave()
    {
        waitingForNextWave = true;

        yield return new WaitForSeconds(timeBetweenWaves);

        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            yield break;
        }

        waitingForNextWave = false;

        StartWave();
    }
}
