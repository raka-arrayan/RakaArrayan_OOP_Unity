using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 2.0f;//menentukan kecepatan musuh bergerak secara horizontal.
    protected bool movingRight;//menandakan apakah musuh bergerak ke arah kanan atau tidak.

    protected override void Start()
    {
        base.Start();

        if (Random.Range(0, 2) == 0)//menghasilkan angka acak antara 0 dan 1
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z); 
            movingRight = true;
        }
        else
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z); 
            movingRight = false;
        }
        //Nilai -10 dan 10 digunakan sebagai batas layar kiri dan kanan, tetapi bisa disesuaikan dengan ukuran layar game.
    }

    protected void Update()//untuk menggerakkan musuh secara horizontal di layar
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x > 8)
            {
                movingRight = false;//musuh akan bergerak ke kiri
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x < -8)
            {
                movingRight = true;//musuh akan bergerak ke kanan 
            }
        }
    }
}
