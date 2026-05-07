using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementHandler : MonoBehaviour
    {
        [Header("Hover Settings (Spring)")]
        [SerializeField] private float targetHeight = 1.75f;
        [SerializeField] private float springStiffness = 100f;
        [SerializeField] private float springDamping = 10f;
        [Space]
        [SerializeField] private LayerMask groundLayer;

        [Header("Descent Settings")]
        [SerializeField] private float fallMultiplier = 10f;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 50f;
        [SerializeField] private float maxSpeed = 15f;
        [SerializeField] private float drag = 10f;

        [Header("Rotation Settings")]
        [SerializeField] private float rotationSensitivity = 2f;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            _rb.linearDamping = drag;
            _rb.useGravity = true;
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        public void ApplySpringHover()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, targetHeight + 2f, groundLayer))
            {
                float compression = targetHeight - hit.distance;

                if (compression > 0)
                {
                    float springForce = compression * springStiffness - _rb.linearVelocity.y * springDamping;

                    _rb.AddForce(Vector3.up * springForce, ForceMode.Acceleration);
                }
                else if (_rb.linearVelocity.y < 0)
                {
                    _rb.AddForce(Vector3.down * fallMultiplier, ForceMode.Acceleration);
                }
            }
        }

        public void ApplyDescentGravity()
        {
            if (_rb.linearVelocity.y < 0)
                _rb.AddForce(Vector3.down * fallMultiplier, ForceMode.Acceleration);
        }

        public void Move(Vector3 direction)
        {
            if (direction.sqrMagnitude > 0.01f)
            {
                _rb.AddForce(direction * moveSpeed, ForceMode.Acceleration);

                Vector3 vel = _rb.linearVelocity;
                Vector3 horizontalVel = new Vector3(vel.x, 0, vel.z);

                if (horizontalVel.magnitude > maxSpeed)
                {
                    horizontalVel = horizontalVel.normalized * maxSpeed;
                    _rb.linearVelocity = new Vector3(horizontalVel.x, vel.y, horizontalVel.z);
                }
            }
        }

        public void RotateBody(float mouseX)
        {
            transform.Rotate(Vector3.up * mouseX * rotationSensitivity);
        }
    }
}