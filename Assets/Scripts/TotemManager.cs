using System.Collections.Generic;
using UnityEngine;

public class TotemManager : MonoBehaviour
{
    public static TotemManager Instance {get; set;}
    [SerializeField] private GameObject totemPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform player;

    public int totensRemainig = 8;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Instancia errada");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (totemPrefab == null || spawnPoints == null)
        {
            Debug.LogError("Erro na configuração");
            enabled = false;
            return;
        }    

        StartTotens();
    }

    private void StartTotens()
    {
        List<Transform> avaiableSpawns = new List<Transform>(spawnPoints);

        for (int i = 0; i <= spawnPoints.Length; i++)
        {
            if (avaiableSpawns.Count == 0)
            {
                avaiableSpawns.AddRange(spawnPoints);
            }

            int index = Random.Range(0, avaiableSpawns.Count);

            Transform spawnPoint = avaiableSpawns[index];

            spawnPoint.position += player.position;

            GameObject totemObject = Instantiate(totemPrefab, spawnPoint.position, Quaternion.identity);

            spawnPoint.position -= player.position;

            avaiableSpawns.RemoveAt(index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (totensRemainig <= -1)
        {
            totensRemainig = 8;
            StartTotens();
        }
    }
}
