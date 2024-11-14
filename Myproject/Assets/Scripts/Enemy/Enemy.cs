using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Level;//level dari musuh

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Start ini akan dipanggil satu kali ketika objek musuh pertama kali diaktifkan dalam game.
    protected virtual void Start()
    {
        //menambahkan komponen Rigidbody2D ke objek
        rb = gameObject.AddComponent<Rigidbody2D>();
        //menambahkan komponen SpriteRenderer ke objek untuk menampilkan sprite.
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        
        rb.isKinematic = true;//Berarti musuh tidak akan terpengaruh oleh gaya atau benturan fisik
        rb.gravityScale = 0;//menonaktifkan gravitasi

        
        spriteRenderer.sprite = Resources.Load<Sprite>("path_to_your_sprite");

        
        GameObject player = GameObject.FindGameObjectWithTag("Player");//mencari objek pemain di dalam game dengan tag "Player".
        if (player != null)
        {
            //maka skrip menghitung arah dari musuh ke pemain
            Vector3 direction = player.transform.position - transform.position;
            transform.right = -direction;
        }
    }

    //dipanggil ketika musuh bertabrakan dengan objek lain.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        }
    }
}
