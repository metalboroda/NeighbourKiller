using System.Collections;
using Assets.Scripts.EventBus;
using EventsFolder;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private Transform weaponPivot;
        
        private WeaponData currentWeaponData;
        private int _currentAmmo;
        private float _nextFireTime;
        private bool _isReloading;
        private WeaponInstance _spawnedWeaponInstance;
        private Transform _currentShootPoint;

        private Camera _camera;
        private PlayerInput _input;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _input = GetComponent<PlayerInput>();

            EquipWeapon(currentWeaponData);
        }

        private void Update()
        {
            if (!currentWeaponData || _isReloading) return;
            if (_input.IsFiring && Time.time >= _nextFireTime)
            {
                if (_currentAmmo > 0)
                    TryShoot();
                else
                    StartCoroutine(ReloadRoutine());
            }

            if (Input.GetKeyDown(KeyCode.R) && _currentAmmo < currentWeaponData.maxAmmo)
                StartCoroutine(ReloadRoutine());
        }

        private void TryShoot()
        {
            _nextFireTime = Time.time + currentWeaponData.fireRate;
            _currentAmmo--;

            currentWeaponData.Shoot(_currentShootPoint, _camera);

            EventBus<WeaponEvents.WeaponFiredEventArgs>.Raise(new WeaponEvents.WeaponFiredEventArgs());

            NotifyAmmoChange();
        }

        public void EquipWeapon(WeaponData newWeapon)
        {
            if (!newWeapon) return;
            if (_spawnedWeaponInstance) Destroy(_spawnedWeaponInstance.gameObject);

            currentWeaponData = newWeapon;
            _currentAmmo = currentWeaponData.maxAmmo;

            if (currentWeaponData.weaponPrefab && weaponPivot)
            {
                GameObject weaponGo = Instantiate(currentWeaponData.weaponPrefab, weaponPivot);

                _spawnedWeaponInstance = weaponGo.GetComponent<WeaponInstance>();

                if (_spawnedWeaponInstance)
                    _currentShootPoint = _spawnedWeaponInstance.ShootPoint;
                else
                    _currentShootPoint = weaponPivot;
            }

            NotifyAmmoChange();
        }

        private IEnumerator ReloadRoutine()
        {
            _isReloading = true;

            Debug.Log("Перезарядка...");

            yield return new WaitForSeconds(currentWeaponData.reloadTime);

            _currentAmmo = currentWeaponData.maxAmmo;
            _isReloading = false;

            NotifyAmmoChange();

            Debug.Log("Перезарядка завершена!");
        }

        private void NotifyAmmoChange()
        {
            EventBus<WeaponEvents.AmmoChangedEventArgs>.Raise(new WeaponEvents.AmmoChangedEventArgs
            {
                currentAmmo = _currentAmmo,
                maxAmmo = currentWeaponData.maxAmmo
            });
        }
    }
}