using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        public void Interact(GameObject user);
        
        public  string GetInteractPrompt();
    }
}