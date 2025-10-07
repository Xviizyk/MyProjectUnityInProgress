using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Что спавним")]
    public GameObject prefabToSpawn;

    [Header("Границы карты")]
    public float minX = -100f;
    public float maxX = 100f;
    public float minY = -100f;
    public float maxY = 100f;

    [Header("Количество врагов (случайное)")]
    public int minEnemies = 50;
    public int maxEnemies = 200;

    [Header("Группы врагов")]
    [Range(0f, 1f)]
    public float groupSpawnChance = 0.3f;
    public int minGroupSize = 3;
    public int maxGroupSize = 7;
    public float groupRadius = 5f;

    void Start()
    {
        SpawnEnemiesAcrossMap();
    }

    void SpawnEnemiesAcrossMap()
    {
        if (prefabToSpawn == null) return;
        int totalEnemies = Random.Range(minEnemies, maxEnemies + 1);
        int spawned = 0;
        while (spawned < totalEnemies)
        {
            bool spawnGroup = Random.value < groupSpawnChance;

            if (spawnGroup)
            {
                int groupSize = Random.Range(minGroupSize, maxGroupSize + 1);
                groupSize = Mathf.Min(groupSize, totalEnemies - spawned);
                float centerX = Random.Range(minX, maxX);
                float centerY = Random.Range(minY, maxY);
                Vector3 centerPos = new Vector3(centerX, centerY, 0);
                for (int i = 0; i < groupSize; i++)
                {
                    Vector2 offset = Random.insideUnitCircle * groupRadius;
                    Vector3 spawnPos = centerPos + new Vector3(offset.x, offset.y, 0);

                    Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
                    spawned++;
                }
            }
            else
            {
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);
                Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                spawned++;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0),
            new Vector3(maxX - minX, maxY - minY, 0)
        );
    }
}
