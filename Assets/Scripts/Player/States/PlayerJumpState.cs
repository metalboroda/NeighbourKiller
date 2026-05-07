using UnityEngine;

namespace Player.States
{
    public class PlayerJumpState : PlayerBaseState
    {
        public override void Enter()
        {
            Handler.Jump();
        }

        public override void FixedUpdate()
        {
            Handler.RotateBody(Input.MouseDelta.x);
            Handler.Move(Context.transform.TransformDirection(Input.MoveVector));
            
            if (Context.GetComponent<Rigidbody>().linearVelocity.y <= 0.1f)
                Context.ChangeState<PlayerInAirState>();
        }
    }
}