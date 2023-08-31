
namespace Assets.Scripts
{
    public abstract class NodeBaseState
    {
        public abstract void EnterState(NodeStateManager node);
        public abstract void OnClick(NodeStateManager node);
    }
}
