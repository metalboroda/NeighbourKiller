namespace Player.States
{
    public class PlayerInAirState : PlayerBaseState
    {
        public override void FixedUpdate()
        {
            Movement.RotateBody(Input.MouseDelta.x);
            Movement.Move(Context.transform.TransformDirection(Input.MoveVector));
            Movement.ApplyDescentGravity();

            if (Movement.IsGrounded)
                Context.ChangeState<PlayerHoverState>();
        }

        public override void LateUpdate()
        {
            Camera.RotateCamera(Input.MouseDelta.y);
        }
    }
}