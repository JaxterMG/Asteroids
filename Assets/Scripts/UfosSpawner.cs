using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfosSpawner : Spawner
{
    public override void SpawnByIndex(int index)
    {
        GameObject ufo = PoolManager.Instance.GetPoolingObjectByType(PoolingItemType.Ufo);

        ufo.transform.position = _spawnPoints[index].transform.position;
        ufo.GetComponent<Ufo>().GetRandomDirection();
    }
}
