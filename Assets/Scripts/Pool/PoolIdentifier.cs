using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    [System.Serializable]
    public class PoolIdentifier
    {
        public string Name;
        public PoolingItemType Type;
        public GameObject Prefab;
    }
}