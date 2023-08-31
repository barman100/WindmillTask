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
            if (_nodesPerStage.Length < _stagesAmount)
                throw new ArgumentException("Missing values, _stagesAmount is larger then _nodesPerStage", "_nodesPerStage");

            if (_nodesPerStage[0] != 1)
                throw new ArgumentException("First Nodes Per Stage value must be a single node");

            InitializeParams();


            if (_verticalDistance < _shapeZoffSet)
                throw new ArgumentException($"{_maxNodes} is too many Nodes,Please lower the amount of nodes or increase the Plane vertical size");

            if (_horizontalDistance < _shapeXoffSet)
                throw new ArgumentException($"{_stagesAmount} is too many stages, Please lower the amount of stages or increase the Plane horizontal size");

            var OffSet = _nodesPerStage[0] * _verticalDistance / 2;

            GenerateMap(OffSet);

        }

        private void GenerateMap(float OffSet)
        {
            for (int i = 0; i < _stagesAmount; i++)
            {
                var OffSetHorizontal = i * _horizontalDistance;
                var MaxDistanace = _nodesPerStage[i] * _verticalDistance;
                var Distance = 0f;

                Distance = (MaxDistanace / 2);

                for (int j = 0; j < _nodesPerStage[i]; j++)
                {
                    var spawnPosition = new Vector3((_planePosition.position.x - _planeXbound / 2) + OffSetHorizontal, 0, _planePosition.position.z - Distance + OffSet);
                    Instantiate(_nodePrefab, spawnPosition, Quaternion.identity);
                    Distance -= MaxDistanace / _nodesPerStage[i];
                }
            }
        }

        private void InitializeParams()
        {
            _planeXbound = _planeRenderer.bounds.size.x;
            _planeZbound = _planeRenderer.bounds.size.z;
            _shapeXoffSet = _nodePrefabRenderer.bounds.size.x;
            _shapeZoffSet = _nodePrefabRenderer.bounds.size.z;

            _horizontalDistance = (_planeXbound - _shapeXoffSet) / _stagesAmount;
            _maxNodes = 0;
            foreach (var nodesAmount in _nodesPerStage)
            {
                _maxNodes = nodesAmount > _maxNodes ? nodesAmount : _maxNodes;
            }
            _verticalDistance = (_planeZbound - _shapeZoffSet) / _maxNodes;
        }
    }
}
