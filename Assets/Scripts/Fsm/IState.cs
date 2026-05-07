namespace Assets.Scripts.Fsm
{
    public interface IState<TContext> where TContext : class
    {
        void Setup(TContext context);
        void Enter();
        void Exit();
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}