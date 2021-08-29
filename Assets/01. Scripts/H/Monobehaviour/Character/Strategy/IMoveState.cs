using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveState
{
    void OperateEnter(Rigidbody2D _rb2d, Transform _transform, float _speed);
    void OperateUpdate(Rigidbody2D _rb2d, Transform _transform, float _speed);
    void OperateExit(Rigidbody2D _rb2d, Transform _transform, float _speed);
}

class IMoveState_MoveToDestination : IMoveState
{
    public void OperateEnter(Rigidbody2D _rb2d, Transform _transform, float _speed)
    {
        
    }

    public void OperateExit(Rigidbody2D _rb2d, Transform _transform, float _speed)
    {

    }

    public void OperateUpdate(Rigidbody2D _rb2d, Transform _transform, float _speed)
    {
        Vector2 Destination = (_transform.position - _rb2d.transform.position);
        _rb2d.MovePosition(_rb2d.position + (Destination * _speed * Time.deltaTime));
    }
}
