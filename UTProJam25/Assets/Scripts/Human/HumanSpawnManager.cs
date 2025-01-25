using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class HumanSpawnManager : MonoBehaviour
{
    [FormerlySerializedAs("enemyPrefab")] public GameObject humanPrefab; // The enemy prefab to spawn
    private BoxCollider2D spawnArea; // Reference to the spawn area
    public int numberOfEnemies = 5; // Number of enemies to spawn
    private float spawnCooldown = 2f;
    private readonly float spawnMaxCooldown = 2f;
    [SerializeField]
    private bool spawnUsingTimer;

    void Start()
    {
        spawnArea = GameObject.Find("HumanSpawnArea").GetComponent<BoxCollider2D>();
        SpawnEnemies();
    }

    private void Update()
    {
        if (spawnUsingTimer)
        {
            if (spawnCooldown <= 0)
            {
                SpawnEnemy();
                spawnCooldown = spawnMaxCooldown;
            }
            else
            {
                spawnCooldown -= Time.deltaTime;
            }
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPoint = GetRandomPointInArea();
        Instantiate(humanPrefab, spawnPoint, Quaternion.identity);
    }

    Vector2 GetRandomPointInArea()
    {
        // Get bounds of the spawn area
        Bounds bounds = spawnArea.bounds;

        // Generate a random point within the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }

    // Optional: To visualize the spawn area in the editor
    private void OnDrawGizmos()
    {
        if (spawnArea != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(spawnArea.bounds.center, spawnArea.bounds.size);
        }
    }
}
