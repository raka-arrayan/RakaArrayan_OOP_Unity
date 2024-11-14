using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 2.0f;
    private bool movingDown;//menandakan apakah musuh sedang bergerak ke bawah atau tidak.

    
    protected override void Start()
    {
        base.Start();

        
        if (Random.Range(0, 2) == 0)//menghasilkan angka acak antara 0 dan 1
        {
            
            transform.position = new Vector3(transform.position.x, 8, transform.position.z); 
            movingDown = true;//musuh akan muncul di sisi atas layar dengan posisi y pada 8, dan akan bergerak ke bawah
        }
        else
        {
            
            transform.position = new Vector3(transform.position.x, -8, transform.position.z); 
            movingDown = false;//musuh akan muncul di sisi bawah layar dengan posisi y pada -8, dan akan bergerak ke atas
        }
    }

    
    void Update()//dipanggil setiap frame untuk menggerakkan musuh secara vertikal di layar
    {
        //Kode ini mengatur pergerakan vertikal musuh di layar dengan arah bolak-balik antara batas atas dan bawah.
        
        if (movingDown)
        {
            
            if (transform.position.y > -8)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
            else
            {
                
                transform.position = new Vector3(transform.position.x, -8, transform.position.z); 
                movingDown = false;
            }
        }
        else
        {
            
            if (transform.position.y < 8)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                
                transform.position = new Vector3(transform.position.x, 8, transform.position.z); 
                movingDown = true;
            }
        }
    }
}
