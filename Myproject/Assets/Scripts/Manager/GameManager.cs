using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //GameManager dirancang untuk mengikuti pola desain Singleton.
    //Singleton memastikan bahwa hanya ada satu instance dari GameManager di seluruh permainan.
    

    public LevelManager LevelManager { get; private set; }
    //private set hanya bisa diatur (diubah) dari dalam kelas GameManager
    //get bersifat public, objek lain tetap bisa menggunakan LevelManager tetapi tidak bisa mengubahnya

    public int points { get; private set; }  // Nilai poin pemain


    //kode ini menjaga agar GameManager tetap menjadi singleton
    //yaitu hanya ada satu instance yang aktif sepanjang permainan.
    // Jika ada instance GameManager lain yang tidak disengaja, instance duplikat ini akan langsung dihancurkan
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        LevelManager = GetComponentInChildren<LevelManager>();

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }


    // Fungsi untuk menambah poin
    public void AddPoints(int amount)
    {
        points += amount;
        // Panggil untuk memperbarui UI setelah poin bertambah
        GameUIController uiController = FindObjectOfType<GameUIController>();
        if (uiController != null)
        {
            uiController.UpdatePoints(points);
        }
    }
}
