using Assets.Scripts.Fsm;
using Player.States;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovementHandler Handler { get; private set; }
        public PlayerInput InputProvider { get; private set; }
        public PlayerCameraHandler CameraHandler { get; private set; }

        private FiniteStateMachine<PlayerController> _fsm;
        private StateFactory<PlayerController> _stateFactory;

        private void Awake()
        {
            Handler = GetComponent<PlayerMovementHandler>();
            InputProvider = GetComponent<PlayerInput>();
            CameraHandler = GetComponent<PlayerCameraHandler>();

            _fsm = new FiniteStateMachine<PlayerController>(this);
            _stateFactory = new StateFactory<PlayerController>(this);
        }

        private void Start()
        {
            _fsm.Initialize(_stateFactory.GetState<PlayerHoverState>());
        }

        private void Update() => _fsm.CurrentState?.Update();
        private void FixedUpdate() => _fsm.CurrentState?.FixedUpdate();

        public void ChangeState<T>() where T : class, IState<PlayerController>, new()
        {
            _fsm.ChangeState(_stateFactory.GetState<T>());
        }
    }
}