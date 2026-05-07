using Assets.Scripts.Fsm;

namespace Player.States
{
    public abstract class PlayerBaseState : State<PlayerController>
    {
        protected PlayerMovementHandler Handler => Context.Handler;
        protected PlayerInput Input => Context.InputProvider;
    }
}