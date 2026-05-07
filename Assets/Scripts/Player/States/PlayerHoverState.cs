namespace Player.States
{
    public class PlayerHoverState : PlayerBaseState
    {
        public override void FixedUpdate()
        {
            Handler.ApplySpringHover();
            Handler.ApplyDescentGravity();
            Handler.Move(Input.MoveVector);
        }
    }
}