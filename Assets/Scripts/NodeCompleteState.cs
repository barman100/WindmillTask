using UnityEngine;

namespace Assets.Scripts
{
    public class NodeCompleteState : NodeBaseState
    {
        public override void EnterState(NodeStateManager node)
        {
            node.ChangeColor(node.CurrentState);
        }

        public override void OnClick(NodeStateManager node)
        {
            Debug.Log("Node Is already Completed");
        }

    }
}
