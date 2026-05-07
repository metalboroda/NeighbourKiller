using UnityEngine;

namespace Player.States
{
    public class PlayerHoverState : PlayerBaseState
    {
        public override void Update()
        {
            Camera.RotateCamera(Input.MouseDelta.y);
            Handler.RotateBody(Input.MouseDelta.x);
        }

        public override void FixedUpdate()
        {
            Handler.ApplySpringHover();
            Handler.ApplyDescentGravity();

            Vector3 worldDirection = Context.transform.TransformDirection(Input.MoveVector);

            Handler.Move(worldDirection);
        }
    }
}