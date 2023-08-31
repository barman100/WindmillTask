using UnityEngine;
using System;


namespace Assets.Scripts
{
    public class NodeStateManager : MonoBehaviour
    {
        public int index;
        public NodeBaseState _currentState;
        public NodeLockedState _lockedState = new();
        public NodeOpenState _openState = new();
        public NodeCompleteState _completeState = new();
        public event Action<GameObject,NodeBaseState> ChangeColorEvent;
        public event Action<NodeStateManager> OpenNextNodesEvent;

        void Start()
        {
            if (index == 0)
                _currentState = _openState;
            else
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
        public void ChangeColor(NodeBaseState state)
        {
            ChangeColorEvent?.Invoke(gameObject, state);
        }
        public void OpenNextNodes()
        {
            OpenNextNodesEvent?.Invoke(this);
        }
    }
}