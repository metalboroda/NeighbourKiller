using UnityEngine;

namespace Weapons
{
    public class WeaponInstance : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint;

        public Transform ShootPoint => shootPoint;
    }
}