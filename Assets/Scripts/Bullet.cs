using UnityEngine;
using Pool;

public class Bullet : BorderTransferUnit
{
    [SerializeField]
    int _damage = 1;
    [SerializeField]
    float _speed = 50;
    Rigidbody2D _rb;
    protected override void Start()
    {
        base.Start();

    }
    private void Update()
    {
        Teleportation();
    }
    public void GiveDirection(Vector2 direction)
    {

        CancelInvoke();

        Invoke("BringToPool", 3f);
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(direction * _speed, ForceMode2D.Impulse);
    }
    private void BringToPool()
    {
        _rb.velocity = Vector2.zero;
        gameObject.GetComponent<PoolObject>().ReturnToPool();
    }
 
}
