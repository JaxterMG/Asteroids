
using System.Collections;
using System.Collections.Generic;
using Audio;
using Pool;
using UnityEngine;

public class Asteroid : BorderTransferUnit
{
    [SerializeField]
    protected int _health = 3;
    [SerializeField]
    private int _nextIdToSpawn;
    [SerializeField]
    private int _amountToSpawn;
    private AudioSourceHelper _soundManager;
    Rigidbody2D _rb;
    [SerializeField]
    protected float _maxThrust;
    [SerializeField]
    float _maxTorque;
    [SerializeField]
    protected int points;
    public static event System.Action<int> OnDeath;



    protected void Awake()
    {
        _soundManager = GetComponent<AudioSourceHelper>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    protected override void Start()
    {
        base.Start();
        
    }
    protected void Update()
    {
        Teleportation();
    }
    public void GetRandomDirection()
    {
        Vector2 _thrust = new Vector2(Random.Range(-_maxThrust,_maxThrust), Random.Range(-_maxThrust,_maxThrust));
        float _torque = Random.Range(-_maxTorque,_maxTorque);

        _rb.AddForce(_thrust,ForceMode2D.Impulse);
        _rb.AddTorque(_torque);
    }
    public void GetDamage()
    {
        _health -= 1;

        if (_health <= 0)
        {
            AudioSourceHelper.Instance.PlayOneShot(ClipType.Explosion);
            GameObject _explosion = PoolManager.Instance.GetPoolingObjectByType(PoolingItemType.Explosion);
            _explosion.transform.position = transform.position;
            OnDeath?.Invoke(points);
            SpawnAsteroids();
            gameObject.GetComponent<PoolObject>().ReturnToPool();
        }
    }
    private void SpawnAsteroids()
    {
        if (_nextIdToSpawn != -1)
        {
            for (int i = 0; i < _amountToSpawn; i++)
            {
                GameObject _asteroid = PoolManager.Instance.GetPoolingObjectById(PoolingItemType.Obstacle, _nextIdToSpawn);
                _asteroid.transform.position = transform.position;
                _asteroid.transform.rotation = transform.rotation;
                _asteroid.GetComponent<Asteroid>().GetRandomDirection();
            }
            
        }
       

    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            GetDamage();
            collision.gameObject.GetComponent<PoolObject>().ReturnToPool();
        }
    }
      
    


}
