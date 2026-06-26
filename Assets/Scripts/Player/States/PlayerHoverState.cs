namespace Player.States
{
    public class PlayerHoverState : PlayerBaseState
    {
        public override void Update()
        {
            Movement.RotateBody(Input.MouseDelta.x);

            if (Input.JumpPressed && Movement.IsGrounded && Movement.CanJump)
                Context.ChangeState<PlayerJumpState>();
        }

        public override void FixedUpdate()
        {
            Movement.ApplySpringHover();
            Movement.ApplyDescentGravity();
            Movement.Move(Context.transform.TransformDirection(Input.MoveVector));
        }
        
        public override void LateUpdate()
        {
            Camera.RotateCamera(Input.MouseDelta.y);
        }
    }
}