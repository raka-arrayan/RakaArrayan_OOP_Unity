using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;  // Damage yang diberikan oleh Bullet
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;

    // Membuat ObjectPool
    public void SetObjectPool(IObjectPool<Bullet> pool)
    {
        objectPool = pool;
    }

    // Mengambil informasi Rigidbody2D
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Menggerakkan Bullet
        rb.velocity = transform.up * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Mengecek apakah objek yang ditabrak memiliki komponen HealthComponent
        Player player = collider.GetComponent<Player>(); 
        if (player != null)
        {
            // Memberikan damage kepada Player yang tertabrak
            player.TakeDamage(damage); 
        }

        // Menonaktifkan Bullet jika bertabrakan dengan object
        objectPool?.Release(this);
    }

    void OnBecameInvisible()
    {
        // Menonaktifkan Bullet jika Bullet keluar dari layar
        objectPool?.Release(this);
    }
}
