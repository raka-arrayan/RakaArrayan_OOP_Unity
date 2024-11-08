using UnityEngine;

public class Portal : MonoBehaviour
{
    public float speed = 1f; //menentukan kecepatan pergerakan portal.
    public float rotateSpeed = 100f; //menentukan seberapa cepat portal berputar

    private Vector2 newPosition; //posisi baru portal yang akan dituju
    private Player player; //referensi ke objek Player yang ada di scene.
    SpriteRenderer spriteRenderer;//komponen yang mengontrol tampilan sprite portal
    Collider2D portalCollider;//komponen yang berfungsi sebagai collider untuk mendeteksi interaksi dengan objek lain (Player)

    //Menetapkan posisi awal objek.
    //Mengakses atau menginisialisasi variabel atau komponen lain.
    //Mengatur kondisi awal
    void Start()
    {
        //Mengambil komponen SpriteRenderer yang ada pada GameObject portal untuk mengontrol tampilannya.
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Mengambil komponen Collider2D untuk mendeteksi tabrakan
        portalCollider = GetComponent<Collider2D>();
        
        //mengatur posisi awal portal secara acak di area tertentu.
        ChangePosition();
        
        //mendeteksi apakah pemain ada dalam scene.
        player = FindObjectOfType<Player>();
    }


    //Menggerakkan objek berdasarkan input pengguna.
    //memeriksa status game.
    //Menangani perubahan posisi objek dalam game berdasarkan waktu atau input.
    void Update()
    {

        //Portal bergerak menuju posisi baru (newPosition) dengan kecepatan yang ditentukan oleh speed
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        //Portal akan berputar berdasarkan kecepatan rotasi (rotateSpeed). 
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // Portal memeriksa apakah jaraknya dari posisi baru (newPosition) kurang dari 0.5 unit.
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();//memanggil ChangePosition().
        }

        //Jika objek player ditemukan dan objek Player memiliki komponen Weapon 
        //(menandakan bahwa pemain sedang bergerak atau sedang memegang senjata)
        if (player != null && player.GetComponentInChildren<Weapon>() != null)
        {
            //maka  portal akan muncul
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            //Jika tidak, portal disembunyikan (sprite dan collider dinonaktifkan).
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){//jika object tersebut adalah player
            LoadMainScene();//maka LoadMainScene() dipanggil untuk memulai pemuatan scene utama
        }
    }


    void ChangePosition()
    {
        //memberikan kesan bahwa portal bergerak secara acak di dunia permainan.
        newPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }

    void LoadMainScene (){
        //memanggil fungsi LoadScene() dari LevelManager untuk memuat scene dengan nama "Main".
        GameManager.Instance.LevelManager.LoadScene("Main");
    }
}
