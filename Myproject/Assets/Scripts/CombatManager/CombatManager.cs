using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Array untuk menyimpan referensi ke spawner musuh
    public EnemySpawner[] enemySpawners;
    // Timer untuk menghitung waktu antar gelombang musuh
    public float timer = 0;
    // Interval waktu antara setiap gelombang musuh (dalam detik)
    [SerializeField] private float waveInterval = 5f;
    // Menyimpan nomor gelombang yang sedang berjalan
    public int waveNumber = 1;
    // Menyimpan jumlah total musuh yang sudah di-spawn
    public int totalEnemies = 0;

    void Update()
    {
        // Menambahkan waktu yang telah berlalu pada timer
        timer += Time.deltaTime;

        // Jika timer sudah melebihi waveInterval, maka spawn musuh baru
        if (timer >= waveInterval)
        {
            // Spawn musuh dari setiap spawner dalam enemySpawners
            foreach (var spawner in enemySpawners)
            {
                spawner.SpawnEnemy();
            }
             // Increment waveNumber untuk menandakan bahwa gelombang baru dimulai
            waveNumber++;
            // Reset timer agar siap menghitung waktu untuk gelombang berikutnya
            timer = 0;
        }
    }

    void StartNewWave()
    {
        waveNumber++;

        GameUIController uiController = FindObjectOfType<GameUIController>();
        if (uiController != null)
        {
            uiController.UpdateWave(waveNumber); // Perbarui teks Wave di UI
        }
    }

    void UpdateEnemiesCount()
    {
        int enemiesLeft = FindObjectsOfType<Enemy>().Length;

        GameUIController uiController = FindObjectOfType<GameUIController>();
        if (uiController != null)
        {
            uiController.UpdateEnemiesLeft(enemiesLeft); // Perbarui teks jumlah musuh di UI
        }
    }


}
