using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public TextMeshProUGUI healthText;      // Teks untuk Health
    public TextMeshProUGUI pointsText;      // Teks untuk Points
    public TextMeshProUGUI waveText;        // Teks untuk Wave
    public TextMeshProUGUI enemiesLeftText; // Teks untuk Enemies Left

    private int points = 0;   // Poin awal
    private int health = 50; // health awal
    private int waveNumber = 1; // Nomor wave awal
    private int enemiesLeft = 10; // Jumlah musuh yang tersisa

    // Menginisialisasi UI dengan nilai awal
    void Start()
    {
        Debug.Log("GameUIController Start dipanggil");
        UpdateHealth(health);
        UpdatePoints(points);
        UpdateWave(waveNumber);
        UpdateEnemiesLeft(enemiesLeft);
    }

    void Awake()
    {
        Debug.Log("GameUIController Awake dipanggil");
        // Pastikan GameUIController tidak dihancurkan saat scene berpindah
        DontDestroyOnLoad(this.gameObject);// mempertahankan data UI antar scene.
    }

    // Perbarui teks Health
    public void UpdateHealth(int newHealth)
    {
        health = newHealth; // Mengupdate nilai health 
        healthText.text = "Health: " + health.ToString(); // Menampilkan health di UI
    }

    // Perbarui teks Points
    public void UpdatePoints(int newPoints)
    {
        points = newPoints; // Mengupdate nilai poin
        pointsText.text = "Points: " + points.ToString(); // Menampilkan poin di UI
    }

    public int GetPoints()
    {
        return points;
    }

    // Perbarui teks Wave
    public void UpdateWave(int newWave)
    {
        waveNumber = newWave; // Mengupdate nomor wave
        waveText.text = "Wave: " + waveNumber.ToString(); // Menampilkan nomor wave di UI
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public void NextWave()//menaikkan nomor wave saat musuh di wave sebelumnya telah selesai.
    {
        waveNumber++;
        UpdateWave(waveNumber);
        Debug.Log("Wave baru dimulai: Wave " + waveNumber);
    }

    // Perbarui teks Enemies Left
    public void UpdateEnemiesLeft(int newEnemiesLeft)
    {
        enemiesLeft = newEnemiesLeft; // Mengupdate jumlah musuh yang tersisa
        enemiesLeftText.text = "Enemies Left: " + enemiesLeft.ToString(); // Menampilkan musuh yang tersisa di UI
    }


    public void DecreaseEnemiesLeft()//mengurangi jumlah musuh setiap kali satu musuh dikalahkan
    {
        if (enemiesLeft > 0)
        {
            enemiesLeft--; // Mengurangi jumlah musuh
            UpdateEnemiesLeft(enemiesLeft); // Memperbarui UI
        }

        // Cek apakah semua musuh dalam wave telah dikalahkan
        if (enemiesLeft == 0)
        {
            // Mulai wave baru
            NextWave();
        }
    }
}
