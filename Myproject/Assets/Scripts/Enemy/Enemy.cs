using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;
    public Sprite enemySprite;
    protected Rigidbody2D rb;
    public float moveSpeed = 2f;

    public Camera mainCamera;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Tidak ada camera pada " + this);
            }
        }
    }

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public virtual void Move()
    {
    }

    void Update()
    {
        Move();
    }

    void OnDestroy()
    {
        GameUIController uiController = FindObjectOfType<GameUIController>();//mencari objek di scene yang memiliki komponen GameUIController
        if (uiController != null)
        {
            // Update points setelah musuh dihancurkan
            uiController.UpdatePoints(uiController.GetPoints() + level);
            uiController.DecreaseEnemiesLeft();//memanggil  DecreaseEnemiesLeft() dari GameUIController
        }
        else
        {
            Debug.LogWarning("GameUIController tidak ditemukan saat musuh dihancurkan!");
        }
    }
}
