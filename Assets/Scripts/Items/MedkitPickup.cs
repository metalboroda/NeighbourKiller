using Interfaces;
using UnityEngine;

namespace Items
{
    public class MedkitPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private float healAmount = 25f;

        public void Interact(GameObject user)
        {
            // Наприклад, якщо у гравця є скрипт PlayerHealth
            // if (user.TryGetComponent(out PlayerHealth health)) { health.Heal(healAmount); Destroy(gameObject); }
        }

        public string GetInteractPrompt() => "Take Medkit (+25 HP)";
    }
}