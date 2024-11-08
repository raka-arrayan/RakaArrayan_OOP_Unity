using UnityEngine;

public class PlayerMovement : MonoBehaviour //class ini bertanggung jawab untuk mengatur gerakan pemain
{
    private Rigidbody2D playerRigidBody; //Variabel ini bertipe Rigidbody2D, digunakan untuk mengontrol  objek 2D

    [SerializeField] private Vector2 maxSpeed = new Vector2(7, 5);
    // kecepatan maksimum yang dapat dicapai Player
    [SerializeField] private Vector2 timeToFullSpeed = new Vector2(1, 1);
    //waktu yang diperlukan Player untuk mencapai kecepatan maksimum ketika diberikan input
    [SerializeField] private Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    //waktu yang diperlukan Player untuk berhenti ketika tidak lagi diberikan input
    [SerializeField] private Vector2 stopClamp = new Vector2(2.5f, 2.5f);
    //batas kecepatan minimum sebelum Player berhenti bergerak

    //menggunakan [SerializeField] agar menandai variabel agar dapat diserialisasi dan terlihat di Inspector Unity
    //mengguanakan Vector2 digunakan untuk merepresentasikan titik atau vektor dalam ruang 2D.

    private Vector2 moveDirection; // arah gerakan Player dalam bentuk vektor sesuai dengan input
    private float moveVelocityX; //Kecepatan gerakan pada sumbu X (horizontal).
    private float moveVelocityY; // Kecepatan gerakan pada sumbu Y (vertikal).
    private float movingFrictionX; //Gesekan saat bergerak pada sumbu X (horizontal).
    private float movingFrictionY; //Gesekan saat bergerak pada sumbu Y (vertikal)
    private float stopFrictionX; //Gesekan saat berhenti pada sumbu X (horizontal)
    private float stopFrictionY; //Gesekan saat berhenti pada sumbu Y (vertikal)

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>(); //Mengambil dan menyimpan komponen Rigidbody2D dari GameObject
        moveVelocityX = (2 * maxSpeed.x) / timeToFullSpeed.x;
        moveVelocityY = (2 * maxSpeed.y) / timeToFullSpeed.y;
        movingFrictionX = (-2 * maxSpeed.x) / Mathf.Pow(timeToFullSpeed.x, 2);
        movingFrictionY = (-2 * maxSpeed.y) / Mathf.Pow(timeToFullSpeed.y, 2);
        stopFrictionX = (-2 * maxSpeed.x) / Mathf.Pow(timeToStop.x, 2);
        stopFrictionY = (-2 * maxSpeed.y) / Mathf.Pow(timeToStop.y, 2);
    }

    void FixedUpdate()
    {
        Move(); //Memanggil method Move untuk mengatur gerakan pemain.
    }

    public void Move()
    //method ini bertanggung jawab untuk membuat Player (Pesawat) dapat bergerak sesuai dengan input yang diberikan
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W)) verticalInput = 1f;
        //W untuk naik (vertikal positif)
        if (Input.GetKey(KeyCode.S)) verticalInput = -1f;
        //S untuk turun (vertikal negatif).
        if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;
        //D untuk bergerak ke kanan (horizontal positif).
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
        //A untuk bergerak ke kiri (horizontal negatif).

        Debug.Log($"Horizontal Input: {horizontalInput}, Vertical Input: {verticalInput}");
        //Berfungsi untuk mencetak informasi ke konsol di Unity

        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        //Membuat arah gerakan dan menormalkannya sehingga kecepatan tidak melebihi batas.
        playerRigidBody.velocity = moveDirection * maxSpeed.x;
        //Mengatur kecepatan objek berdasarkan arah gerakan dan kecepatan maksimum.

        // Menentukan batas pergerakan dengan offset dari viewport kamera
        Vector2 minLimit = (Vector2)Camera.main.ViewportToWorldPoint(Vector2.zero) + new Vector2(0.225f, 0.1005f);
        Vector2 maxLimit = (Vector2)Camera.main.ViewportToWorldPoint(Vector2.one) - new Vector2(0.225f, 0.500f);

        //ViewportToWorldPoint mengonversi koordinat dari "viewport space" ke "world space."
        //World space adalah sistem koordinat global untuk seluruh dunia permainan.baik 2D atau 3D
        //Vector2.zero menunjukkan sudut kiri bawah layar (koordinat viewport (0,0)), 
        //yang kemudian dikonversi menggunakan ViewportToWorldPoint.
        //Vector2.zero menunjukkan sudut kiri bawah layar (koordinat viewport (0,0)), yang kemudian dikonversi ke koordinat dunia menggunakan ViewportToWorldPoint.
        //Vector2.one mewakili sudut kanan atas layar (koordinat viewport (1,1)).


        // Membatasi posisi pesawat agar tetap berada di dalam batas kamera yang sudah disesuaikan
        transform.position = new Vector2(
        Mathf.Clamp(transform.position.x, minLimit.x, maxLimit.x),
        Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y)
        );
        //fungsi Mathf.Clamp membatasi nilai transform.position.x agar selalu berada di antara minLimit.x dan maxLimit.x, yaitu batas kiri dan kanan.
        //Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y) melakukan hal yang sama untuk nilai y (batas atas dan bawah).
        //transform.position kemudian diperbarui dengan nilai yang sudah dibatasi, sehingga objek pesawat tidak akan melewati batas yang ditentukan dalam permainan.

    }

    private Vector2 CalculateFriction()
    { //method ini me-return gaya gesekan berdasarkan apakah pemain sedang bergerak atau tidak.
        return moveDirection != Vector2.zero ? //memeriksa apakah moveDirection tidak sama dengan Vector2.zero.
        //seandainya bener dia gak 0 ,maka akan return vektor yang berisi nilai gesekan saat bergerak (membaca dari movingFrictionX dan movingFrictionY).
            new Vector2(movingFrictionX, movingFrictionY) : 
        // jika salah maka akan return vektor yang berisi nilai gesekan saat berhenti (membaca dari stopFrictionX dan stopFrictionY).
            new Vector2(stopFrictionX, stopFrictionY);
            
//Vector2.zero merepresentasikan vektor (0, 0), yang berarti tidak ada gerakan (baik ke kiri/kanan maupun atas/bawah).
    }

    public bool IsMoving()
    {
        return playerRigidBody.velocity.magnitude > 0;
        //method ini me-return true jika pemain sedang bergerak (kecepatan lebih besar dari nol) dan false jika tidak.
    }
}
