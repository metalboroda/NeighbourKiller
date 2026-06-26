using UnityEngine;

namespace Player.States
{
    public class PlayerJumpState : PlayerBaseState
    {
        public override void Enter()
        {
            Movement.Jump();
        }

        public override void FixedUpdate()
        {
            Movement.RotateBody(Input.MouseDelta.x);
            Movement.Move(Context.transform.TransformDirection(Input.MoveVector));

            if (Context.GetComponent<Rigidbody>().linearVelocity.y <= 0.1f)
                Context.ChangeState<PlayerInAirState>();
        }
    }
}