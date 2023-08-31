﻿using UnityEngine;

namespace Assets.Scripts
{
    public class NodeLockedState : NodeBaseState
    {
        public override void EnterState(NodeStateManager node)
        {
            Debug.Log($"EnterState {node.gameObject.name} in state: {node._currentState}");
        }

        public override void OnClick(NodeStateManager node)
        {
            Debug.Log($"OnClick {node.gameObject.name} in state: {node._currentState}");
        }

    }
}
