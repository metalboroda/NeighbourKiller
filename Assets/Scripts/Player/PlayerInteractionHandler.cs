using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionHandler : MonoBehaviour
    {
        [Header("Detection Settings")]
        [SerializeField] private float interactionDistance = 3.0f;
        [Space]
        [SerializeField] private LayerMask interactionLayer;
        [Space]
        [SerializeField] private KeyCode interactKey = KeyCode.E;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactionLayer))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    // ТУТ можна передавати текст у твій UI менеджер:
                    // UIManager.Instance.ShowPrompt(interactable.GetInteractPrompt());

                    if (Input.GetKeyDown(interactKey))
                        interactable.Interact(gameObject);
                }
            }
        }
    }
}