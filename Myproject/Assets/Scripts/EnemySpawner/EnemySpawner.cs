using UnityEngine;
// Kelas abstrak yang menjadi dasar untuk semua spawner musuh
public abstract class EnemySpawner : MonoBehaviour
{
    // Metode abstrak yang harus diimplementasikan oleh kelas turunannya
    // Setiap kelas yang mewarisi EnemySpawner harus mendefinisikan bagaimana musuh akan di-spawn
    public abstract void SpawnEnemy();
}
