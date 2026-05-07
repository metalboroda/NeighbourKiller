using System;

namespace Assets.Scripts.Fsm
{
    public interface IFiniteStateMachine<TContext> where TContext : class
    {
        event Action<IState<TContext>> StateChanged;

        IState<TContext> CurrentState { get; }
        IState<TContext> PreviousState { get; }

        void Initialize(IState<TContext> initialState);
        void ChangeState(IState<TContext> newState, bool forceChange = false);
        void ChangeStateWithDelay(IState<TContext> newState, float delay, bool forceChange = false);
        void StopDelayedStateChange();
        void Lock();
        void Unlock();
    }
}