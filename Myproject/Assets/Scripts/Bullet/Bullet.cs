using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;

    private Rigidbody2D rb;
    private IObjectPool<Bullet> bulletPool; // Referensi ke Object Pool

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Ambil komponen Rigidbody2D
    }

    // Set Object Pool untuk Bullet
    public void SetObjectPool(IObjectPool<Bullet> pool)
    {
        bulletPool = pool; // Set pool untuk Bullet
    }

    // Tembakkan Bullet
    public void Fire()
    {
        rb.velocity = transform.up * bulletSpeed; // Set kecepatan Bullet
    }

    // Cek saat Bullet menabrak objek atau keluar layar
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Kembalikan Bullet ke pool saat menabrak objek
        ReturnBulletToPool();
    }

    // Fungsi untuk mengembalikan Bullet ke pool
    public void ReturnBulletToPool()
    {
        if (bulletPool != null)
        {
            bulletPool.Release(this); // Kembalikan Bullet ke pool
        }
        else
        {
            Debug.LogError("Bullet Pool is not set!"); // Debug jika pool tidak diset
        }
    }

    // Fungsi untuk menangani Bullet yang keluar dari layar
    void OnBecameInvisible()
    {
        ReturnBulletToPool(); // Kembalikan Bullet ke pool saat keluar dari layar
    }
}