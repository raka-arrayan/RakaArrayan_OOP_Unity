using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    void Awake()
    {
        // Inisialisasi weapon dengan nilai weaponHolder yang sudah di-assign di Inspector
        weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false); // Menonaktifkan visual senjata awalnya
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // Memastikan bahwa objek yang memasuki trigger adalah Player
        if (other.gameObject.CompareTag("Player"))
        {
            Weapon weapon_now = other.GetComponentInChildren<Weapon>();

            if (weapon_now != null){
                TurnVisual(false,weapon_now);
            }
            
            Debug.Log("Objek Player Memasuki trigger, senjata akan diambil.");

            // Mengubah parent weapon ke Player agar senjata mengikuti Player
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = Vector3.zero; // Atur posisi senjata agar sesuai dengan posisi Player

            // Aktifkan visual senjata setelah di-equip
            TurnVisual(true, weapon);

        }
    }

    private void TurnVisual(bool on)
    {
        // Fungsi untuk mengaktifkan atau menonaktifkan visual dari weapon
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        // Fungsi overloading untuk mengaktifkan visual tertentu dari weapon yang spesifik
        weapon.gameObject.SetActive(on);
    }
}