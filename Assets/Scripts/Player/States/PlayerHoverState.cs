namespace Player.States
{
    public class PlayerHoverState : PlayerBaseState
    {
        public override void Update()
        {
            Camera.RotateCamera(Input.MouseDelta.y);
            Handler.RotateBody(Input.MouseDelta.x);

            if (Input.JumpPressed && Handler.IsGrounded && Handler.CanJump)
                Context.ChangeState<PlayerJumpState>();
        }

        public override void FixedUpdate()
        {
            Handler.ApplySpringHover();
            Handler.ApplyDescentGravity();
            Handler.Move(Context.transform.TransformDirection(Input.MoveVector));
        }
    }
}