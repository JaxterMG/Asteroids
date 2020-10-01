using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : Asteroid
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Bullet>() || collision.gameObject.GetComponent<Asteroid>() )
        {
            GetDamage();
            collision.gameObject.GetComponent<PoolObject>().ReturnToPool();
        }

    }
}
