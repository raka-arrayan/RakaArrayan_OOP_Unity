using UnityEngine;

public class EnemyHorizontalSpawner : EnemySpawner
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public float speed = 3f;
    private float screenLimitX;

    void Start()
    {
        screenLimitX = Camera.main.aspect * Camera.main.orthographicSize;
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    public override void SpawnEnemy()
    {
        bool spawnLeft = Random.value > 0.5f;
        float spawnX = spawnLeft ? -screenLimitX - 1f : screenLimitX + 1f;
        Vector3 spawnPosition = new Vector3(spawnX, Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize), 0);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.AddComponent<EnemyHorizontalMover>().Initialize(spawnLeft, speed);
    }
}


public class EnemyHorizontalMover : MonoBehaviour
{
    private float speed;
    private bool movingRight;

    public void Initialize(bool spawnLeft, float movementSpeed)
    {
        movingRight = spawnLeft;
        speed = movementSpeed;
    }

    void Update()
    {
        float direction = movingRight ? 1f : -1f;
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > Camera.main.aspect * Camera.main.orthographicSize + 1f)
        {
            movingRight = !movingRight;
        }
    }
}
