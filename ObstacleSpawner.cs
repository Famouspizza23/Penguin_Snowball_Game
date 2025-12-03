using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject[] obstaclePrefabs;
    public float spawnDistance = 50f;
    public float spawnInterval = 10f;

    private float lastSpawnZ = 50f;
    private bool canSpawn = false;
    private float spawnTimer = 0f;

    private float[] lanes = new float[] { -4f, 0f, 4f };

    
    

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            canSpawn = true;

            if (player.position.z + spawnDistance > lastSpawnZ && canSpawn)
            {
                SpawnObstacleRow();
                lastSpawnZ += spawnInterval;
            }
        }


        
    }
    void SpawnObstacleRow()
    {
        if (canSpawn)
        {
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            int laneIndex = Random.Range(0, lanes.Length);
            float x = lanes[laneIndex];

            Vector3 pos = new Vector3(x, 0f, lastSpawnZ);
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
