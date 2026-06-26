using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector3 MoveVector { get; private set; }
        public Vector2 MouseDelta { get; private set; }
        public bool JumpPressed => Input.GetButtonDown("Jump"); 
        public bool IsFiring => Input.GetButton("Fire1");

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
            MouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}