using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    public Transform parentTransform;
    public WeaponPickup weaponPickup;

    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    void Awake()
    {
        // Membuat object pool
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );

        // Memeriksa dan mengatur BulletSpawnPoint
        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = transform.Find("BulletSpawnPoint");
            if (bulletSpawnPoint == null)
            {
                Debug.LogWarning($"{name}: BulletSpawnPoint tidak ditemukan, membuat spawn point baru.");
                GameObject spawnPoint = new GameObject("BulletSpawnPoint");
                spawnPoint.transform.parent = transform;
                spawnPoint.transform.localPosition = new Vector3(0, 1, 0);
                bulletSpawnPoint = spawnPoint.transform;
            }
        }
    }

    // Membuat Bullet baru untuk ObjectPool
    private Bullet CreateBullet()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError($"{name}: BulletPrefab belum diatur!");
            return null;
        }

        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.SetObjectPool(objectPool);
        return newBullet;
    }

    // Mengaktifkan Bullet untuk digunakan
    private void OnGetBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = bulletSpawnPoint.position; // Reset posisi bullet
            bullet.transform.rotation = bulletSpawnPoint.rotation; // Reset rotasi bullet
        }
    }

    // Menonaktifkan Bullet yang tidak digunakan
    private void OnReleaseBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            bullet.gameObject.SetActive(false);
        }
    }

    // Menghancurkan Bullet jika ObjectPool penuh
    private void OnDestroyBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Menembakkan Bullet pada interval waktu tertentu
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0;
        }
    }

    public Bullet Shoot()
    {
        if (objectPool != null)
        {
            Bullet bulletInstance = objectPool.Get(); // Mengambil Bullet dari ObjectPool
            return bulletInstance;
        }
        else
        {
            Debug.LogWarning($"{name}: ObjectPool tidak diinisialisasi.");
            return null;
        }
    }
}
