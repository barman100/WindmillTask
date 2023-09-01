using UnityEngine;

namespace Assets.Scripts
{
    public class NodeOpenState : NodeBaseState
    {
        public override void EnterState(NodeStateManager node)
        {
            node.ChangeColor(node.CurrentState);
        }

        public override void OnClick(NodeStateManager node)
        {
            node.ChangeState(node.CompleteState);
            node.OpenNextNodes();
        }

    }
}
