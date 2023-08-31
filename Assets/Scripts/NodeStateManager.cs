using UnityEngine;

namespace Assets.Scripts
{
    public class NodeStateManager : MonoBehaviour
    {
        public NodeBaseState _currentState;
        public NodeLockedState _lockedState = new();
        public NodeOpenState _openState = new();
        public NodeCompleteState _completeState = new();

        void Start()
        {
            _currentState = _lockedState;
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
        }
    }
}