using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector3 MoveVector { get; private set; }

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            
            MoveVector = new Vector3(x, 0, z).normalized;
        }
    }
}