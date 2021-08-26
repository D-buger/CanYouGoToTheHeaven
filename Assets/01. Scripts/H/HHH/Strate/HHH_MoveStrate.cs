using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HHH_MoveStrate
{
    void OperateEnter(Rigidbody2D _rb2d, GameObject _gameobject, float _speed);
    void OperateUpdate(Rigidbody2D _rb2d, GameObject _gameobject, float _speed);
    void OperateExit(Rigidbody2D _rb2d = null, GameObject _gameobject = null, float _speed = 0);
}

class HHH_MoveStrate_TrackingTarget : HHH_MoveStrate
{
    public void OperateEnter(Rigidbody2D _rb2d, GameObject _gameobject, float _speed)
    {
        
    }

    public void OperateExit(Rigidbody2D _rb2d = null, GameObject _gameobject = null, float _speed = 0)
    {
        
    }

    public void OperateUpdate(Rigidbody2D _rb2d, GameObject _gameobject, float _speed)
    {
        Vector2 targetDirection = (_rb2d.transform.position - _gameobject.transform.position);
        _rb2d.position = Vector2.MoveTowards(_rb2d.position, _gameobject.transform.position, _speed * Time.deltaTime);
    }
}

class HHH_MoveStrate_Static : HHH_MoveStrate
{
    public void OperateEnter(Rigidbody2D _rb2d, GameObject _gameobject, float _speed)
    {
        
    }

    public void OperateExit(Rigidbody2D _rb2d = null, GameObject _gameobject = null, float _speed = 0)
    {
        
    }

    public void OperateUpdate(Rigidbody2D _rb2d, GameObject _gameobject, float _speed)
    {
        
    }
}
