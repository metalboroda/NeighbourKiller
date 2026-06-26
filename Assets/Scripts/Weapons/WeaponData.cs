using Interfaces;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("Visuals")]
        public string weaponName;
        [Space]
        public GameObject weaponPrefab;
        public GameObject muzzleFlashPrefab;

        [Header("Shooting Settings")]
        public float fireRate = 0.2f;
        public float damage = 10f;
        public float range = 100f;
        [Space]
        public LayerMask hitLayers;

        [Header("Ammo Settings")]
        public int maxAmmo = 30;
        public float reloadTime = 1.5f;
        
        public virtual void Shoot(Transform shootPoint, Camera playerCamera)
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            
            if (Physics.Raycast(ray, out RaycastHit hit, range, hitLayers))
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
                else
                {
                    Debug.Log($"Влучили в {hit.collider.name}, але цей об'єкт не отримує шкоди.");
                }
            }
            
            if (shootPoint && muzzleFlashPrefab)
            {
                GameObject flash = Instantiate(muzzleFlashPrefab, shootPoint.position, shootPoint.rotation);
                
                Destroy(flash, 0.5f);
            }
        }
    }
}