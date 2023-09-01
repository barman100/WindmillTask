using UnityEngine;
using System;


namespace Assets.Scripts
{
    public class NodeStateManager : MonoBehaviour
    {
        public int Index;
        public NodeBaseState CurrentState { get; private set; }
        public NodeLockedState LockedState { get; private set; }
        public NodeOpenState OpenState { get; private set; }
        public NodeCompleteState CompleteState { get; private set; }

        public event Action<GameObject,NodeBaseState> ChangeColorEvent;
        public event Action<NodeStateManager> OpenNextNodesEvent;

        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            LockedState = new();
            OpenState = new();
            CompleteState = new();

            if (Index == 0)
                CurrentState = OpenState;
            else
                CurrentState = LockedState;

            CurrentState.EnterState(this);
        }

        private void OnMouseDown()
        {
            CurrentState.OnClick(this);
        }
        public void ChangeState(NodeBaseState state)
        {
            CurrentState = state;
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