using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f; // Interval antara tembakan

    [Header("Bullets")]
    public Bullet bulletPrefab; // Bullet yang akan ditembakkan
    [SerializeField] private Transform bulletSpawnPoint; // Titik spawn untuk Bullet

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool; // Object Pool untuk Bullet

    // Konfigurasi pool
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;

    private float timer; // Timer untuk tembakan
    public Transform parentTransform; // Transform dari parent untuk Bullet

    void Start()
    {
        // Membuat Object Pool untuk Bullet
        objectPool = new ObjectPool<Bullet>(
            CreatePooledBullet,   // Fungsi untuk membuat Bullet baru
            OnTakeFromPool,      // Fungsi saat mengambil Bullet dari pool
            OnReturnToPool,      // Fungsi saat mengembalikan Bullet ke pool
            OnDestroyBullet,     // Fungsi saat Bullet dihancurkan
            collectionCheck,     // Tidak perlu memeriksa koleksi
            defaultCapacity,     // Kapasitas awal pool
            maxSize              // Kapasitas maksimal pool
        );
    }

    // Membuat Bullet baru
    private Bullet CreatePooledBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab); // Membuat instance baru dari Bullet
        bullet.SetObjectPool(objectPool);           // Set pool untuk Bullet
        return bullet;
    }

    // Fungsi saat Bullet diambil dari pool
    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true); // Mengaktifkan Bullet saat diambil
    }

    // Fungsi saat Bullet dikembalikan ke pool
    private void OnReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); // Menonaktifkan Bullet saat dikembalikan
    }

    // Fungsi untuk menghancurkan Bullet saat pool penuh
    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject); // Hancurkan Bullet jika pool penuh
    }

    // Update untuk memeriksa interval tembakan
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            FireBullet(); // Panggil fungsi tembak jika waktu sudah mencapai interval
            timer = 0f; // Reset timer
        }
    }

    // Fungsi untuk menembakkan Bullet
    void FireBullet()
    {
        Bullet bullet = objectPool.Get(); // Ambil Bullet dari pool
        bullet.transform.position = bulletSpawnPoint.position; // Set posisi spawn Bullet
        bullet.Fire(); // Tembak Bullet
    }
}