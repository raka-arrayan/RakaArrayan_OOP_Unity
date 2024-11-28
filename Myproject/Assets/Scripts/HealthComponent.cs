using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int maxHealth = 10;

    private int health;

    void Awake()
    {
        health = maxHealth;
    }

    public void Subtract(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }

//coba
    public void SubtractAndUpdateUI(int amount)
    {
        health -= amount;

        // Mendapatkan referensi GameUIController
        GameUIController uiController = FindObjectOfType<GameUIController>();
        if (uiController != null)
        {
            uiController.UpdateHealth(health);
        }
        else
        {
            Debug.LogWarning("GameUIController tidak ditemukan!");
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
