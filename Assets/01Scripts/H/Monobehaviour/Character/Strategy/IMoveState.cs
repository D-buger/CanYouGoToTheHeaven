using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveState
{
    void OperateEnter(Rigidbody2D _rb2d, Transform _transform, float _speed);
    void OperateUpdate(Rigidbody2D _rb2d, Transform _transform, float _speed);
    void OperateExit(Rigidbody2D _rb2d, Transform _transform, float _speed);
}

class IMoveState_Static : IMoveState
{
    public void OperateEnter(Rigidbody2D _rb2d = null, Transform _transform = null, float _speed = 0)
    {
        Debug.Log($"Start: Static State");
    }

    public void OperateExit(Rigidbody2D _rb2d = null, Transform _transform = null, float _speed = 0)
    {
        Debug.Log($"Stop: Static State");
    }

    public void OperateUpdate(Rigidbody2D _rb2d = null, Transform _transform = null, float _speed = 0)
    {
        
    }
}

class IMoveState_MoveToDestination : IMoveState
{
    public void OperateEnter(Rigidbody2D _rb2d, Transform _destination, float _speed)
    {
        
    }

    public void OperateExit(Rigidbody2D _rb2d, Transform _destination, float _speed)
    {

    }

    public void OperateUpdate(Rigidbody2D _rb2d, Transform _destination, float _speed)
    {
        Vector2 destination = (_destination.position - _rb2d.transform.position);
        _rb2d.MovePosition(_rb2d.position + (destination * _speed * Time.deltaTime));
    }
}
