using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class PoolManager : MonoBehaviour
    {
        private Dictionary<PoolingItemType, List<PoolPart>> _pools = new Dictionary<PoolingItemType, List<PoolPart>>();

        [SerializeField] private PoolIdentifier[] _originals;

        private static PoolManager _instance;
        public static PoolManager Instance => _instance;

        private void Awake()
        {
            _instance = this;

            for (int i = 0; i < (int)PoolingItemType.Count; i++)
            {
                _pools.Add((PoolingItemType)i, new List<PoolPart>());
            }

            foreach (var poolingItem in _originals)
            {
                _pools[poolingItem.Type].Add(new PoolPart(poolingItem.Prefab, poolingItem.Name));
            }
        }

        public GameObject GetPoolingObjectByType(PoolingItemType type)
        {
            return _pools[type][Random.Range(0, _pools[type].Count)].GetObject();
        }
        public GameObject GetPoolingObjectById(PoolingItemType type, int id)
        {
            return _pools[type][id].GetObject();
        }
    }
}
