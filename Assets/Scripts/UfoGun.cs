using Audio;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoGun : MonoBehaviour
{
    [SerializeField]
    private float _timeBetweenShots;
    [SerializeField]
    private float _angleOffset;
    private float _timer;
    private Transform _player;


    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>().transform;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer>_timeBetweenShots)
        {
            _timer = 0;
            var _direction = Aim();
            Fire(_direction);
        }
    }
    private Vector2 Aim()
    {
        var _direction = (_player.position - transform.position).normalized;
        return Quaternion.AngleAxis(Random.Range(-_angleOffset, _angleOffset), Vector2.up) * _direction;
    }
    private void Fire(Vector2 _direction)
    {
        AudioSourceHelper.Instance.PlayOneShot(ClipType.Shot);
        GameObject _laser = PoolManager.Instance.GetPoolingObjectByType(PoolingItemType.EnemyProjectile);
        _laser.transform.position = transform.position;
        _laser.GetComponent<Bullet>().GiveDirection(_direction);
    }

}
