using UnityEngine;

public class WeaponPickup : MonoBehaviour// WeaponPickup berfungsi menangani logika pengambilan senjata
{
    [SerializeField] private Weapon weaponHolder;
    //ditandai [SerializeField] agar bisa diset di Unity Inspector.
    private Weapon weapon;

    void Awake()
    {
        //weapon di-inisialisasi dengan membuat salinan (Instantiate) dari weaponHolder
        weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        if (weapon != null)//Jika weapon telah berhasil diinstansiasi
        {
            TurnVisual(false); 
            //menonaktifkan visual dari senjata tersebut (senjata tidak akan terlihat sebelum diambil pemain.)
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // Memastikan bahwa objek yang memasuki trigger adalah Player
        if (other.gameObject.CompareTag("Player"))
        {//Mengecek apakah objek yang masuk adalah pemain (Player) berdasarkan tag


            Weapon weapon_now = other.GetComponentInChildren<Weapon>();
            //Mengecek apakah pemain sudah memiliki senjata. Jika ada, visual dari senjata yang sebelumnya dipegang oleh pemain akan dimatikan

            if (weapon_now != null){
                TurnVisual(false,weapon_now);
            }
            
            Debug.Log("Objek Player Memasuki trigger, senjata akan diambil.");

            // Mengubah parent weapon ke Player agar senjata mengikuti Player
            weapon.transform.SetParent(other.transform);//Mengubah induk (parent) dari senjata menjadi pemain agar senjata mengikuti pemain.
            weapon.transform.localPosition = Vector3.zero; // Atur posisi senjata agar sesuai dengan posisi Player

            // Aktifkan visual senjata setelah di-equip
            TurnVisual(true, weapon);

        }
    }



    //Jika on bernilai true, senjata akan terlihat; jika false, senjata tidak akan terlihat.
    private void TurnVisual(bool on)
    {
        // Fungsi untuk mengaktifkan atau menonaktifkan visual dari weapon
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }


    //untuk mengaktifkan atau menonaktifkan visual dari senjata tertentu yang diberikan sebagai parameter.
    private void TurnVisual(bool on, Weapon weapon)
    {
        weapon.gameObject.SetActive(on);
    }
}