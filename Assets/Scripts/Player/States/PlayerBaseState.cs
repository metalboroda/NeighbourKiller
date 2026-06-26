using Assets.Scripts.Fsm;

namespace Player.States
{
    public abstract class PlayerBaseState : State<PlayerController>
    {
        protected PlayerMovementHandler Movement => Context.Handler;
        protected PlayerInput Input => Context.InputProvider;
        protected PlayerCameraHandler Camera => Context.CameraHandler;
    }
}