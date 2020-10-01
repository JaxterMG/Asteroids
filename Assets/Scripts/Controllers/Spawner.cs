using Pool;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private protected GameObject[] _spawnPoints;
    [Range(1, 5)]
    [SerializeField] private protected  int _obstaclesNumberInOneWawe;
    [SerializeField] private protected float _timeBetweenWawes;


    protected float _timeFromLastWawe;

   protected private void Awake()
    {
        if (_spawnPoints == null)
        {
            throw new Exception("spawn points null exception");
        }

        _timeFromLastWawe = 0.0f;
    }

    private protected void Update()
    {
        if (_timeFromLastWawe > 0)
        {
            _timeFromLastWawe -= Time.deltaTime;
        }
        else
        {
            _timeFromLastWawe = _timeBetweenWawes;
            SpawnWawe();
        }
    }

    private protected async void SpawnWawe()
    {
        for (int i = 0; i < _obstaclesNumberInOneWawe; i++)
        {
            SpawnByIndex(UnityEngine.Random.Range(0, _spawnPoints.Length));
            await Task.Delay(300);
        }
    }

    public virtual void SpawnByIndex(int index)
    {
        GameObject obstacle = PoolManager.Instance.GetPoolingObjectByType(PoolingItemType.Obstacle);

        obstacle.transform.position = _spawnPoints[index].transform.position;
        obstacle.GetComponent<Asteroid>().GetRandomDirection();
    }
}