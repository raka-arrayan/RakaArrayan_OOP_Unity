using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform player;
    private float speed = 2f;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        // Mendapatkan transform pemain dengan tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Mendapatkan batas layar menggunakan kamera
        minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
        minY = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1f;
        maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;

        // Menentukan posisi spawn musuh secara acak di sepanjang batas layar
        float spawnX = Random.value < 0.5f ? minX : maxX;
        float spawnY = Random.Range(minY, maxY);
        transform.position = new Vector3(spawnX, spawnY, transform.position.z);
    }

    void Update()
    {
        // Jika pemain ada di dalam scene
        if (player != null)
        {
            // Menghitung arah menuju pemain dan normalisasi agar kecepatannya konstan
            Vector3 direction = (player.position - transform.position).normalized;

            // Menggerakkan musuh menuju pemain
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    // Ketika musuh bertabrakan dengan pemain
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jika yang bersentuhan adalah objek dengan tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Hancurkan musuh
            Destroy(gameObject);
        }
    }
}
