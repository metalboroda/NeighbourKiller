using Interfaces;
using UnityEngine;

namespace Interactions
{
    public class DoorInteractive : MonoBehaviour, IInteractable
    {
        private bool _isOpen;

        public void Interact(GameObject user)
        {
            _isOpen = !_isOpen;
        }

        public string GetInteractPrompt() => _isOpen ? "Close Door" : "Open Door";
    }
}