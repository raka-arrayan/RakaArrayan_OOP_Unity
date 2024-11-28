using UnityEngine;

public class EnemyForwardSpawner : EnemySpawner
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public float speed = 3f;

    void Start()
    {
        //Memanggil method SpawnEnemy secara berulang dengan interval yang ditentukan
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    public override void SpawnEnemy()
    {
        // Menghitung posisi spawn di sepanjang sumbu Y di luar layar
        float spawnY = Camera.main.orthographicSize + 1f;
        // Menentukan posisi spawn pada sumbu X secara acak di antara batas kiri dan kanan layar
        Vector3 spawnPosition = new Vector3(Random.Range(-Camera.main.aspect * Camera.main.orthographicSize, Camera.main.aspect * Camera.main.orthographicSize), spawnY, 0);

        // Menginstantiate musuh dari prefab dan menambahkannya komponen EnemyForwardMover untuk menangani pergerakan
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.AddComponent<EnemyForwardMover>().Initialize(speed);
    }
}

public class EnemyForwardMover : MonoBehaviour
{
    private float speed;// Kecepatan pergerakan musuh

    public void Initialize(float movementSpeed)
    {
        speed = movementSpeed;//Menyimpan kecepatan yang diterima dari EnemyForwardSpawner
    }

    void Update()
    {
        // Menambahkan pergerakan musuh ke bawah sepanjang sumbu Y
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Destroy the enemy if it goes off-screen
        if (transform.position.y < -Camera.main.orthographicSize - 1f)
        {
            Destroy(gameObject);
        }
    }
}
