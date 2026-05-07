using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Fsm
{
    public class FiniteStateMachine<TContext> : IFiniteStateMachine<TContext> where TContext : class
    {
        public event Action<IState<TContext>> StateChanged;

        public IState<TContext> CurrentState { get; private set; }
        public IState<TContext> PreviousState { get; private set; }

        private bool _isLocked;
        private Coroutine _stateChangeCoroutine;
        private readonly MonoBehaviour _coroutineRunner;

        public FiniteStateMachine(MonoBehaviour coroutineRunner)
        {
            _coroutineRunner = coroutineRunner ?? throw new ArgumentNullException(nameof(coroutineRunner), "MonoBehaviour runner cannot be null for FSM operations requiring coroutines.");
        }

        public void Initialize(IState<TContext> initialState)
        {
            if (initialState == null)
            {
                throw new ArgumentNullException(nameof(initialState), "Initial state cannot be null.");
            }
            if (CurrentState != null)
            {
                Debug.LogWarning("[FSM] FSM is already initialized. Ignoring call.");
                return;
            }

            CurrentState = initialState;
            CurrentState.Enter();

            StateChanged?.Invoke(CurrentState);

            // Debug.Log($"[FSM] Initialized. Current State: {CurrentState.GetType().Name}");
        }

        public void ChangeState(IState<TContext> newState, bool forceChange = false)
        {
            if (newState == null)
            {
                Debug.LogError("[FSM] Cannot change to a null state.");
                return;
            }

            if (!forceChange)
            {
                if (_isLocked)
                {
                    Debug.LogWarning($"[FSM] State change blocked for {newState.GetType().Name}. FSM is locked.");
                    return;
                }

                if (newState == CurrentState)
                {
                    // Debug.Log($"[FSM] State change ignored. New state {newState.GetType().Name} is the same as current.");
                    return;
                }
            }

            StopDelayedStateChangeInternal();
            PerformStateChange(newState);
        }

        public void ChangeStateWithDelay(IState<TContext> newState, float delay, bool forceChange = false)
        {
            if (newState == null)
            {
                Debug.LogError("[FSM] Cannot change to a null state with delay.");
                return;
            }

            if (delay <= 0)
            {
                Debug.LogWarning($"[FSM] Delay is zero or negative ({delay}s). Changing state immediately.");

                ChangeState(newState, forceChange);
                return;
            }

            if (!forceChange)
            {
                if (_isLocked)
                {
                    Debug.LogWarning($"[FSM] Delayed state change blocked for {newState.GetType().Name}. FSM is locked.");
                    return;
                }
                if (newState == CurrentState)
                {
                    // Debug.Log($"[FSM] Delayed state change ignored. New state {newState.GetType().Name} is the same as current.");
                    return;
                }
            }

            if (_stateChangeCoroutine != null)
            {
                if (!forceChange)
                {
                    Debug.LogWarning($"[FSM] Another delayed state change is already pending. Ignoring request for {newState.GetType().Name}. Use forceChange=true to override.");
                    return;
                }
                StopDelayedStateChangeInternal();
            }

            _stateChangeCoroutine = _coroutineRunner.StartCoroutine(DoChangeStateWithDelay(newState, delay, forceChange));
        }

        public void StopDelayedStateChange()
        {
            StopDelayedStateChangeInternal();
        }

        public void Lock()
        {
            _isLocked = true;

            // Debug.Log("[FSM] State machine locked.");
        }

        public void Unlock()
        {
            _isLocked = false;

            // Debug.Log("[FSM] State machine unlocked.");
        }

        private void PerformStateChange(IState<TContext> newState)
        {
            // Debug.Log($"[FSM] Changing state from {CurrentState?.GetType().Name ?? "None"} to {newState.GetType().Name}");

            PreviousState = CurrentState;

            try
            {
                CurrentState?.Exit();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[FSM] Exception during Exit of state {PreviousState?.GetType().Name}: {ex}");
            }


            CurrentState = newState;

            try
            {
                CurrentState.Enter();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[FSM] Exception during Enter of state {CurrentState?.GetType().Name}: {ex}");
            }

            try
            {
                StateChanged?.Invoke(CurrentState);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[FSM] Exception during StateChanged event handler for state {CurrentState?.GetType().Name}: {ex}");
            }
        }

        private IEnumerator DoChangeStateWithDelay(IState<TContext> newState, float delay, bool forceChange)
        {
            yield return new WaitForSeconds(delay);

            if (!forceChange)
            {
                if (_isLocked)
                {
                    Debug.LogWarning($"[FSM] Delayed state change cancelled for {newState.GetType().Name}. FSM was locked during delay.");

                    _stateChangeCoroutine = null;
                    yield break;
                }
            }

            if (newState != CurrentState || forceChange)
            {
                PerformStateChange(newState);
            }
            else
            {
                Debug.Log($"[FSM] Delayed state change cancelled for {newState.GetType().Name}. State became the same during delay.");
            }


            _stateChangeCoroutine = null;
        }

        private void StopDelayedStateChangeInternal()
        {
            if (_stateChangeCoroutine != null)
            {
                if (_coroutineRunner)
                {
                    _coroutineRunner.StopCoroutine(_stateChangeCoroutine);
                }

                _stateChangeCoroutine = null;

                // Debug.Log("[FSM] Delayed state change coroutine stopped.");
            }
        }
    }
}