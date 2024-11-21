using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    void Awake()
    {
        if (weaponHolder == null)
        {
            Debug.LogError($"{name}: weaponHolder belum diatur di Inspector.");
            return;
        }

        weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Weapon weapon_now = other.GetComponentInChildren<Weapon>();

            if (weapon_now != null)
            {
                TurnVisual(false, weapon_now);
            }

            Debug.Log("Objek Player Memasuki trigger, senjata akan diambil.");

            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = new Vector3(0, 1, 0); // Posisi senjata
            weapon.transform.localRotation = Quaternion.identity;

            TurnVisual(true, weapon);
        }
    }

    private void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}
