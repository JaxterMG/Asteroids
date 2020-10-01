using UnityEngine;

namespace Pool
{
    public class PoolObject : MonoBehaviour
    {
        public PoolPart Owner { get; set; }

        public void ReturnToPool()
        {
            if (Owner != null)
            {
                Owner.AddObject(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}