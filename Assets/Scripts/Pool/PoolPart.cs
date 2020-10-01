using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    [System.Serializable]
    public class PoolPart
    {
        public List<GameObject> PoolingObjects = new List<GameObject>();

        private GameObject _template;

        private string _name;
        public string Name => _name;

        public PoolPart(GameObject original, string name = null)
        {
            _name = name;
            _template = original;
        }

        public GameObject GetObject()
        {
            if (PoolingObjects.Count > 0)
            {
                GameObject returnObject = PoolingObjects[0];
                PoolingObjects.RemoveAt(0);
                returnObject.SetActive(true);
                return returnObject;
            }

            return Instantiate();
        }

        private GameObject Instantiate()
        {
            GameObject @object = GameObject.Instantiate(_template);
            @object.GetComponent<PoolObject>().Owner = this;
            @object.SetActive(true);
            return @object;
        }

        public void AddObject(GameObject newobject)
        {
            newobject.SetActive(false);
            PoolingObjects.Add(newobject);
        }
    }
}