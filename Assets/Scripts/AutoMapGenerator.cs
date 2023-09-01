using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AutoMapGenerator : MonoBehaviour
    {
        [SerializeField] Renderer _planeRenderer;
        [SerializeField] Transform _planePosition;
        [SerializeField] GameObject _nodePrefab;
        [SerializeField] Renderer _nodePrefabRenderer;
        [SerializeField] Transform _spawnPositionParent;
        [SerializeField] int _stagesAmount;
        [Header("First nodes amount (Root) as to be 1")]
        [SerializeField] int[] _nodesPerStage;

        private float _planeXbound;
        private float _planeZbound;
        private float _shapeXoffSet;
        private float _shapeZoffSet;
        private float _horizontalDistance;
        private float _verticalDistance;
        private float _maxNodes;


        void Start()
        {

            Initialize();

            var OffSet = _nodesPerStage[0] * _verticalDistance / 2;

            GenerateMap(OffSet);

            GameManager.NodeController.SetListners();
        }

        /// <summary>
        /// Generating Map based on Parameters values
        /// </summary>
        /// <param name="OffSet"> Minor Offset to keep the nodes Centered</param>
        private void GenerateMap(float OffSet)
        {
            var nodeCounter = 0;
            List<NodeStateManager> nodesListPerStage = new List<NodeStateManager>();
            for (int i = 0; i < _stagesAmount; i++)
            {
                var OffSetHorizontal = i * _horizontalDistance;
                var MaxDistanace = _nodesPerStage[i] * _verticalDistance;
                var Distance = 0f;

                Distance = (MaxDistanace / 2);

                for (int j = 0; j < _nodesPerStage[i]; j++)
                {
                    //Create game Object
                    var spawnPosition = new Vector3((_planePosition.position.x - _planeXbound / 2) + OffSetHorizontal, 0, _planePosition.position.z - Distance + OffSet);
                    var nodeGO = Instantiate(_nodePrefab, spawnPosition, Quaternion.identity, _spawnPositionParent);
                    nodeGO.name = $"Stage{i}: - Node{nodeCounter}";

                    //Setup node's manager in a list
                    var nodeStateManager = nodeGO.GetComponent<NodeStateManager>();
                    nodeStateManager.Index = i;
                    nodesListPerStage.Add(nodeStateManager);

                    nodeCounter++;
                    Distance -= MaxDistanace / _nodesPerStage[i];
                }
                //Add NodeStateManager list to Dicionary using an int index as key 
                GameManager.NodeController.NodesStatesManagersDic.Add(i, CopyList(nodesListPerStage));
                nodesListPerStage.Clear();
            }
        }
        private List<T> CopyList<T>(List<T> source)
        {
            return new List<T>(source);
        }

        /// <summary>
        /// Initialzing parameters values before GenerateMap()
        /// </summary>
        private void Initialize()
        {
            _planeXbound = _planeRenderer.bounds.size.x;
            _planeZbound = _planeRenderer.bounds.size.z;
            _shapeXoffSet = _nodePrefabRenderer.bounds.size.x;
            _shapeZoffSet = _nodePrefabRenderer.bounds.size.z;

            _horizontalDistance = (_planeXbound - _shapeXoffSet) / _stagesAmount;

            if (_horizontalDistance < _shapeXoffSet)
                throw new ArgumentException($"{_stagesAmount} is too many stages, Please lower the amount of stages or increase the Plane horizontal size");

            _maxNodes = 0;

            foreach (var nodesAmount in _nodesPerStage)
            {
                _maxNodes = nodesAmount > _maxNodes ? nodesAmount : _maxNodes;
            }
            _verticalDistance = (_planeZbound - _shapeZoffSet) / _maxNodes;

            if (_verticalDistance < _shapeZoffSet)
                throw new ArgumentException($"{_maxNodes} is too many Nodes,Please lower the amount of nodes or increase the Plane vertical size");

        }
        private void OnValidate()
        {
            if (_nodesPerStage.Length < _stagesAmount)
                throw new ArgumentException("Missing values, _stagesAmount is larger then _nodesPerStage", "_nodesPerStage");

            if (_nodesPerStage[0] != 1)
                throw new ArgumentException("First Nodes Per Stage value must be a single node");

        }
    }
}
