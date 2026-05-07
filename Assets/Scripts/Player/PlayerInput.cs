using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector3 MoveVector { get; private set; }
        public Vector2 MouseDelta { get; private set; }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            
            MoveVector = new Vector3(x, 0, z).normalized;
            
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            MouseDelta = new Vector2(mouseX, mouseY);
        }
    }
}