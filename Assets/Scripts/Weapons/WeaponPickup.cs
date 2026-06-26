using Interfaces;
using UnityEngine;
using Player;

namespace Weapons
{
    [RequireComponent(typeof(Collider))]
    public class WeaponPickup : MonoBehaviour, IInteractable
    {
        [Header("Weapon Data to Give")]
        [SerializeField] private WeaponData weaponData;

        public WeaponData WeaponData => weaponData;

        private void Update()
        {
            transform.Rotate(Vector3.up * (30f * Time.deltaTime));
        }
        
        public void Interact(GameObject user)
        {
            if (user.TryGetComponent(out PlayerWeaponHandler weaponHandler))
            {
                weaponHandler.EquipWeapon(weaponData);
                
                Destroy(gameObject);
                
                Debug.Log($"[Pickup] {user.name} підібрав {weaponData.weaponName}");
            }
        }

        public string GetInteractPrompt()
        {
            return weaponData ? $"Pick up {weaponData.weaponName}" : "Pick up Weapon";
        }
    }
}