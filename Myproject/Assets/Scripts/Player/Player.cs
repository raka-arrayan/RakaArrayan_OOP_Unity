using UnityEngine;
//mengimpor atau menggunakan library UnityEngine
//berfungsi untuk membuat game di Unity. MonoBehaviour, Animator, dan GameObject 

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement; //untuk mengontrol gerakan pemain
    private Animator animator; //untuk mengendalikan animasi efek mesin 
    public static Player Instance { get; private set; } //singleton instance
    private HealthComponent healthComponent;  // Komponen Health untuk mengatur darah pemain

    void Awake()
    {
        // Implementasi Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()//method khusus yang dijalankan sekali ketika game mulai atau ketika objek ini pertama kali muncul di game
    {
        playerMovement = GetComponent<PlayerMovement>();
        //mengontrol gerakan pemain dengan menggunakan function yang ada di PlayerMovement.
        animator = GameObject.Find("EngineEffect")?.GetComponent<Animator>();
        //mencari objek di dalam game yang bernama "EngineEffect" dan mengambil komponen Animator dari objek tersebut
        //Animator ini nantinya akan digunakan untuk mengatur animasi berdasarkan gerakan pemain.
        healthComponent = GetComponent<HealthComponent>();  // Ambil komponen HealthComponent untuk menangani darah pemain
    }

    void FixedUpdate()//method untuk memanggil method Move
    {
        playerMovement.Move();
        //method move itu berfungsi untuk menggerakkan pemain sesuai input yang diterima
    }

    void LateUpdate()
    {
        //LateUpdate dipanggil di akhir setiap frame, setelah semua proses Update selesai
        //berfungsi untuk mengatur nilai Bool dari parameter IsMoving 
        if (animator != null)//Jika animator ada 
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
            // Nilai IsMoving ini diambil dari hasil IsMoving() di playerMovement, 
            //yang mengembalikan true jika pemain sedang bergerak dan false jika tidak bergerak.
            //membuat animasi hanya aktif saat pemain benar-benar bergerak.
        }
    }

    // Method untuk menerima damage dari Bullet
    public void TakeDamage(int damage)
    {
        if (healthComponent != null)
        {
            healthComponent.Subtract(damage);  // Mengurangi darah pemain dengan damage yang diterima
        }
    }
}
