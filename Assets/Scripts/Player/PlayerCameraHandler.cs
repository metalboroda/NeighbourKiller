using UnityEngine;

namespace Player
{
    public class PlayerCameraHandler : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform cameraPivot;
        [Space]
        [SerializeField] private float sensitivity = 2f;
        [SerializeField] private float minPitch = -85f;
        [SerializeField] private float maxPitch = 85f;

        private float _verticalRotation;

        public void RotateCamera(float mouseY)
        {
            if (!cameraPivot) return;
            
            _verticalRotation -= mouseY * sensitivity;
            _verticalRotation = Mathf.Clamp(_verticalRotation, minPitch, maxPitch);
            
            Quaternion targetRotation = Quaternion.Euler(_verticalRotation, 0, 0);
            
            cameraPivot.localRotation = targetRotation;
        }
    }
}