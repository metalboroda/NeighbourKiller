namespace Player.States
{
    public class PlayerInAirState : PlayerBaseState
    {
        public override void FixedUpdate()
        {
            Handler.RotateBody(Input.MouseDelta.x);
            Handler.Move(Context.transform.TransformDirection(Input.MoveVector));
            
            Handler.ApplyDescentGravity();

            if (Handler.IsGrounded)
                Context.ChangeState<PlayerHoverState>();
        }
    }
}