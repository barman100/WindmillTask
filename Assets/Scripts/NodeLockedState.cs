using UnityEngine;

namespace Assets.Scripts
{
    public class NodeLockedState : NodeBaseState
    {
        public override void EnterState(NodeStateManager node)
        {
            node.ChangeColor(node.CurrentState);
        }

        public override void OnClick(NodeStateManager node)
        {
            Debug.Log("Node's is locked");
        }

    }
}
