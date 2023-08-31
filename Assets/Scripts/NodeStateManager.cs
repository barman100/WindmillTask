using UnityEngine;
using System;

namespace Assets.Scripts
{
    public class NodeStateManager : MonoBehaviour
    {
        [SerializeField] int setInitialState;
        public NodeBaseState _currentState;
        public NodeLockedState _lockedState = new();
        public NodeOpenState _openState = new();
        public NodeCompleteState _completeState = new();
        public event Action<GameObject,NodeBaseState> ChangeColor;

        void Start()
        {
            //for testing
            switch (setInitialState)
            {
                case 1:
                    _currentState = _openState;
                    break;
                case 2:
                    _currentState = _lockedState;
                    break;
                    case 3:
                    _currentState = _completeState;
                    break;
                default:
                    _currentState = _lockedState;
                    break;
            }
            //till here
            //_currentState = _lockedState;
            _currentState.EnterState(this);
        }

        private void OnMouseDown()
        {
            _currentState.OnClick(this);
        }
        public void ChangeState(NodeBaseState state)
        {
            _currentState = state;
            state.EnterState(this);
            ChangeColor?.Invoke(gameObject,state);
        }
    }
}