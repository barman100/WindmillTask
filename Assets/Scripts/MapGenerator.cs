using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] GameObject _nodePrefab;
        [SerializeField] Transform _levelPlanTransform;
        [SerializeField] LevelPlan _levelPlan;
        private List<List<Transform>> _nodesPositions = new List<List<Transform>>();
        public void Start()
        {
            Initialize();
            if (_nodesPositions != null)
            {
                GenerateLevelPlan();
            }
        }

        private void GenerateLevelPlan()
        {
            var nodeCounter = 0;
            for (int i = 0; i < _nodesPositions.Count; i++)
            {
                for (int j = 0; j < _nodesPositions[i].Count; j++)
                {
                    Transform transform = _nodesPositions[i][j];
                    GameObject nodeGO = Instantiate(_nodePrefab, transform.position, Quaternion.identity);
                    nodeGO.name = $"Stage{i}: - Node{nodeCounter}";
                    nodeCounter++;
                }
            }
        }

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
    }
}
