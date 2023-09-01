using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] GameObject _nodePrefab;
        [SerializeField] Transform _spawnPositionParent;
        [SerializeField] LevelPlan _levelPlan;
        private List<List<Transform>> _nodesPositions = new List<List<Transform>>();
        public void Start()
        {
            // obviously i can just instantiate the _levelPlan gameobject
            // but to make it easier to plan and prevent human error like forgeting to use the right prefab 
            // or a missing component i decided to use code
            // plus it gives the ability to be more dynamic

            Initialize();

            if (_nodesPositions != null)
            {
                GenerateLevelPlan();
            }
            else if (_nodesPositions.Count == 0)
                throw new ArgumentNullException("_nodesPositions", "_nodesPositions is Empty");
            else
                throw new ArgumentNullException("_nodesPositions", "Couldn't load node's list, _nodesPositions is null");

            GameManager.NodeController.SetListners();
        }
        /// <summary>
        /// Generate map based on LevelPlan Prefab node structure
        /// </summary>
        private void GenerateLevelPlan()
        {
            var nodeCounter = 0;
            List<NodeStateManager> nodesListPerStage = new List<NodeStateManager>();
            for (int i = 0; i < _nodesPositions.Count; i++)
            {
                for (int j = 0; j < _nodesPositions[i].Count; j++)
                {
                    //Create game Object
                    Transform transform = _nodesPositions[i][j];
                    GameObject nodeGO = Instantiate(_nodePrefab, transform.position, Quaternion.identity, _spawnPositionParent);
                    nodeGO.name = $"Stage{i}: - Node{nodeCounter}";

                    //Setup node's manager in a list
                    var nodeStateManager = nodeGO.GetComponent<NodeStateManager>();
                    nodeStateManager.Index = i;
                    nodesListPerStage.Add(nodeStateManager);

                    nodeCounter++;
                }
                //Add NodeStateManager list to Dicionary using an int index as key 
                GameManager.NodeController.NodesStatesManagersDic.Add(i, CopyList(nodesListPerStage));
                nodesListPerStage.Clear();
            }
        }
        /// <summary>
        /// Initialzing list values before GenerateLevelPlan() based on LevelPlan Prefab
        /// </summary>
        private void Initialize()
        {
            _nodesPositions.Add(_levelPlan.RootNodeTransform);

            for (int i = 0; i < _levelPlan.Stages.Length; i++)
            {
                List<Transform> nodes = new List<Transform>();
                var stageTransform = _levelPlan.Stages[i].transform;

                for (int j = 0; j < stageTransform.childCount; j++)
                {
                    nodes.Add(stageTransform.GetChild(j).transform);
                }

                _nodesPositions.Add(nodes);
            }
        }
        private List<T> CopyList<T>(List<T> source)
        {
            return new List<T>(source);
        }
    }
}
