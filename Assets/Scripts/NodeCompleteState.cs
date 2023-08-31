﻿using UnityEngine;

namespace Assets.Scripts
{
    public class NodeCompleteState : NodeBaseState
    {
        public override void EnterState(NodeStateManager node)
        {
            Debug.Log($"{node.gameObject.name} in state: {node._currentState}");

        }

        public override void OnClick(NodeStateManager node)
        {
            Debug.Log($"OnClick {node.gameObject.name} in state: {node._currentState}");
        }

    }
}