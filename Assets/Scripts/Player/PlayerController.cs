using Assets.Scripts.Fsm;
using Player.States;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovementHandler Handler { get; private set; }
        public PlayerInput InputProvider { get; private set; }

        private FiniteStateMachine<PlayerController> _fsm;
        private StateFactory<PlayerController> _stateFactory;

        void Awake()
        {
            Handler = GetComponent<PlayerMovementHandler>();
            InputProvider = GetComponent<PlayerInput>();
            
            _fsm = new FiniteStateMachine<PlayerController>(this);
            _stateFactory = new StateFactory<PlayerController>(this);
        }

        private void Start()
        {
            _fsm.Initialize(_stateFactory.GetState<PlayerHoverState>());
        }

        void Update() => _fsm.CurrentState?.Update();
        void FixedUpdate() => _fsm.CurrentState?.FixedUpdate();
    }
}