using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class NodesController
    {
        public  Dictionary<int, List<NodeStateManager>> NodesStatesManagersDic = new();
        public void SetListners()
        {
            for (int i = 0; i < NodesStatesManagersDic.Count; i++)
            {
                foreach (var node in NodesStatesManagersDic[i])
                {
                    node.ChangeColorEvent += ChangeColor;
                    node.OpenNextNodesEvent += OpenNodes;
                }
            }
        }
        public void ChangeColor(GameObject node, NodeBaseState state)
        {
            switch (state)
            {
                case NodeLockedState:
                    node.GetComponent<Renderer>().material.color = Color.red;
                    break;
                case NodeOpenState:
                    node.GetComponent<Renderer>().material.color = Color.green;
                    break;
                case NodeCompleteState:
                    node.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                default:
                    throw new ArgumentException("Unrecognized State");
            }
        }
        public void OpenNodes(NodeStateManager node)
        {
            if (NodesStatesManagersDic.Count > node.index + 1)
                foreach (var item in NodesStatesManagersDic[node.index + 1])
                {
                    item.ChangeState(node._openState);
                }
        }
    }
}
