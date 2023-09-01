using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class NodesController
    {
        public  Dictionary<int, List<NodeStateManager>> NodesStatesManagersDic = new();
        /// <summary>
        /// Setup Listeners for Node's actions
        /// </summary>
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
        /// <summary>
        /// Change Node's Color and Node's MiniMap color
        /// </summary>
        /// <param name="node"> Node's Game Object</param>
        /// <param name="state">Node's current state</param>
        public void ChangeColor(GameObject node, NodeBaseState state)
        {
            switch (state)
            {
                case NodeLockedState:
                    node.GetComponent<Renderer>().material.color = Color.red;
                    UIManager.ChangeUIColor(node, Color.red);
                    break;
                case NodeOpenState:
                    node.GetComponent<Renderer>().material.color = Color.green;
                    UIManager.ChangeUIColor(node, Color.green);
                    break;
                case NodeCompleteState:
                    node.GetComponent<Renderer>().material.color = Color.blue;
                    UIManager.ChangeUIColor(node, Color.blue);
                    break;
                default:
                    throw new ArgumentException("Unrecognized State");
            }
        }
        /// <summary>
        /// Open next "stage" nodes based on node's index
        /// </summary>
        /// <param name="node">Node's StateManager</param>
        public void OpenNodes(NodeStateManager node)
        {
            var nextIndex = node.Index +1;
            var listIndex = 0;

            if (NodesStatesManagersDic.Count > nextIndex)
                foreach (var item in NodesStatesManagersDic[nextIndex])
                {
                    var nextNode = NodesStatesManagersDic[nextIndex][listIndex];
                    if (nextNode.CurrentState != nextNode.CompleteState)
                        item.ChangeState(node.OpenState);
                    listIndex += listIndex + 1 < NodesStatesManagersDic[nextIndex].Count ? 1 : 0;
                }

        }

    }
}
