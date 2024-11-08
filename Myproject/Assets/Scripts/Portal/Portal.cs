using UnityEngine;

public class Portal : MonoBehaviour
{
    public float speed = 1f; // kecepatan perpindahan portal
    public float rotateSpeed = 100f; // kecepatan rotasi portal

    private Vector2 newPosition; // posisi baru portal
    private Player player; // referensi ke objek Player
    SpriteRenderer spriteRenderer;
    Collider2D portalCollider;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        portalCollider = GetComponent<Collider2D>();
        
        // Mengatur posisi awal portal secara acak
        ChangePosition();
        
        // Mencari objek Player di scene dan mendapatkan komponen Player
        player = FindObjectOfType<Player>();
    }

    void Update()
    {

        // Memindahkan dan merotasi portal
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // Mengecek jika portal sudah sampai pada posisi baru, lalu mengubahnya lagi
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        // Mengaktifkan portal jika pemain sedang bergerak
        if (player != null && player.GetComponentInChildren<Weapon>() != null) // Menggunakan IsMoving dari PlayerMovement
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            LoadMainScene();
        }
    }


    void ChangePosition()
    {
        // Mengubah posisi baru secara acak di area tertentu
        newPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }

    void LoadMainScene (){
        GameManager.Instance.LevelManager.LoadScene("Main");
    }
}
