using UnityEngine;

public class EnemyBossSpawner : EnemySpawner
{
    public GameObject bossPrefab;// Prefab untuk boss yang akan di-spawn
    public float spawnInterval = 10f; // Interval waktu antara setiap spawn boss
    public float speed = 2f; // Kecepatan pergerakan boss setelah di-spawn

    void Start()
    {
        // Mengulang panggilan SpawnEnemy setiap spawnInterval detik
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }
    // Override method SpawnEnemy dari EnemySpawner untuk menambahkan perilaku spesifik
    public override void SpawnEnemy()
    {
        // Menentukan apakah boss akan spawn di sisi kiri atau kanan layar secara acak
        bool spawnLeft = Random.value > 0.5f;
        // Menentukan posisi spawn pada sumbu X, di luar batas layar (tergantung pada sisi spawn)
        float spawnX = spawnLeft ? -Camera.main.aspect * Camera.main.orthographicSize - 1f : Camera.main.aspect * Camera.main.orthographicSize + 1f;
        // Posisi spawn untuk boss, pada sumbu X ditentukan, sumbu Y berada di tengah layar (0)
        Vector3 spawnPosition = new Vector3(spawnX, 0, 0);
        // Menginstansiasi bossPrefab pada posisi spawn dan dengan rotasi identitas (tidak ada rotasi)
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        boss.AddComponent<EnemyBossMover>().Initialize(spawnLeft, speed);
    }
}

public class EnemyBossMover : MonoBehaviour
{
    private float speed;// Kecepatan pergerakan boss
    private bool movingRight;// Menentukan arah gerakan (ke kanan atau kiri)

    public void Initialize(bool spawnLeft, float movementSpeed)
    {
        movingRight = spawnLeft;
        speed = movementSpeed;// Menyimpan kecepatan pergerakan
    }

    void Update()
    {
        // Menghitung arah pergerakan boss berdasarkan nilai movingRight
        float direction = movingRight ? 1f : -1f;
        // Memperbarui posisi boss secara horizontal (ke kanan atau kiri) berdasarkan arah dan kecepatan
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        //Menghancurkan boss jika sudah bergerak keluar dari layar
        if (Mathf.Abs(transform.position.x) > Camera.main.aspect * Camera.main.orthographicSize + 5f)
        {
            Destroy(gameObject);// Menghancurkan objek boss jika posisi X-nya sudah terlalu jauh
        }
    }
}
