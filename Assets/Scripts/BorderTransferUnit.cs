using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTransferUnit : MonoBehaviour
{
    protected Vector2 _borders;
    // Update is called once per frame
    protected virtual void Start()
    {
        _borders = GameManager._instance.GetBorders();
    }
 
    protected void Teleportation()
    {
        Vector2 _newPosition = transform.position;
        if (Mathf.Abs(transform.position.y) > _borders.y)
        {
            _newPosition.y = -transform.position.y;
        }
        if (Mathf.Abs(transform.position.x) > _borders.x)
        {
            _newPosition.x = -transform.position.x;
        }
        transform.position = _newPosition;
    }
 
}
