using System.Collections;
using Pool;
using System;
using UnityEngine;
using GameInput;
using Audio;

public class Player : BorderTransferUnit
{
  
    [SerializeField]
    float _thrust;
    [SerializeField]
    float _xBorder, _yBorder;
    [SerializeField]
    Transform _shotPoint;
    [SerializeField]
    GameObject _bullet;
    [SerializeField]
    Rigidbody2D _rb;
    private int _health = 3;
    [SerializeField]
    private float _defaultTimeBetweenShots=1;
    private float _timeBetweenShots;
    [SerializeField]
    ParticleSystem _engine;
    [SerializeField]
    float _invincibilityTime= 3;
    bool _invincible;
    [SerializeField]
    JoystickButton _shootBtn,_thrustBtn;


    public static event Action<Player> OnPlayerDeath;

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        base.Teleportation();
        if (_thrustBtn._isPressing && !AudioSourceHelper.Instance.IsPlaying())
        {
            AudioSourceHelper.Instance.StartClip();
        }
        else if (!_thrustBtn._isPressing && AudioSourceHelper.Instance.IsPlaying())
        {
            AudioSourceHelper.Instance.StopClip();
        }
        if (_thrustBtn._isPressing && !_engine.isPlaying)
        {
            _engine.Play();
        }
        else if(!_thrustBtn._isPressing&&_engine.isPlaying)
        {
            _engine.Stop();
        }

        if (_shootBtn._isPressing)
        {
            Shoot();
        }
        if(_invincibilityTime>0 && _invincible)
        {
            _invincibilityTime -= Time.deltaTime;

        }
        else if(_invincibilityTime<=0 && _invincible)
        {
            _invincibilityTime = 3f;
            _invincible = false;
        }
      

    }
    private void FixedUpdate()
    {
        if (_thrustBtn._isPressing)
        {
            _rb.AddRelativeForce(Vector2.up *  _thrust);
        }
        float angle = Mathf.Atan2(JoyStickInput._instance._inputDir.x, JoyStickInput._instance._inputDir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }
    void Shoot()
    {
        if (_timeBetweenShots > 0)
        {
            _timeBetweenShots -= Time.deltaTime;
        }
        if (_timeBetweenShots <= 0)
        {
            AudioSourceHelper.Instance.PlayOneShot(ClipType.Shot);
            GameObject _bullet = PoolManager.Instance.GetPoolingObjectByType(PoolingItemType.PlayerProjectile);
            _bullet.transform.position = _shotPoint.position;
            _bullet.transform.rotation = _shotPoint.rotation;
            _bullet.GetComponent<Bullet>().GiveDirection(_shotPoint.position - transform.position);
            _timeBetweenShots = _defaultTimeBetweenShots;
        }
    }
    public int GetHealth()
    {
        return _health;
    }
    void GetDamage()
    {
        if (!_invincible)
        {
            AudioSourceHelper.Instance.PlayOneShot(ClipType.Explosion);
            GameObject _explosion = PoolManager.Instance.GetPoolingObjectByType(PoolingItemType.Explosion);
            _explosion.transform.position = transform.position;
            _health -= 1;
            OnPlayerDeath?.Invoke(this);
            transform.position = Vector3.zero;
            _invincible = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer.Equals(10) || collision.gameObject.layer.Equals(11))
        {
            GetDamage();
        }
    }
}
