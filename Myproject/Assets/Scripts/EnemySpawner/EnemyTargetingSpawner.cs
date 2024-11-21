using UnityEngine;

public class EnemyTargetingSpawner : EnemySpawner
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnInterval = 3f;
    public float speed = 3f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    public override void SpawnEnemy()
    {
        float spawnX = Random.Range(-Camera.main.aspect * Camera.main.orthographicSize, Camera.main.aspect * Camera.main.orthographicSize);
        Vector3 spawnPosition = new Vector3(spawnX, Camera.main.orthographicSize + 1f, 0);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.AddComponent<EnemyTargetingMover>().Initialize(player, speed);
    }
}

public class EnemyTargetingMover : MonoBehaviour
{
    private Transform player;
    private float speed;

    public void Initialize(Transform target, float movementSpeed)
    {
        player = target;
        speed = movementSpeed;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) < 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
