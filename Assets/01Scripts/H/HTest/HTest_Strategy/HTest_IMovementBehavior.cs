using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HTest_IMovementBehavior
{
    void OperateEnter(Rigidbody2D _rb2d, GameObject _gameobject, float _speed);
    void OperateUpdate(Rigidbody2D _rb2d, GameObject _gameobject, float _speed);
    void OperateExit(Rigidbody2D _rb2d = null, GameObject _gameobject = null, float _speed = 0);
}

class HTest_IMovementBehavior_Static : HTest_IMovementBehavior
{
    void HTest_IMovementBehavior.OperateEnter(Rigidbody2D _rb2d, GameObject _target, float _speed)
    {
        Debug.Log($"{_rb2d.gameObject.name}오브젝트가 'Static'상태를 시작합니다");
        _rb2d.velocity = new Vector2(0, 0);
    }

    void HTest_IMovementBehavior.OperateUpdate(Rigidbody2D _rb2d, GameObject _target, float _speed)
    {

    }

    void HTest_IMovementBehavior.OperateExit(Rigidbody2D _rb2d, GameObject _target, float _speed)
    {

    }
}

class HTest_IMovementBehavior_TrackingTarget : HTest_IMovementBehavior
{
    void HTest_IMovementBehavior.OperateEnter(Rigidbody2D _rb2d, GameObject _target, float _speed)
    {
        Debug.Log($"{_rb2d.gameObject.name}오브젝트가 {_target}오브젝트를 추적합니다.");
    }

    void HTest_IMovementBehavior.OperateUpdate(Rigidbody2D _rb2d, GameObject _target, float _speed)
    {
        _rb2d.position = Vector2.MoveTowards(_rb2d.position, _target.transform.position, _speed * Time.deltaTime);
    }

    void HTest_IMovementBehavior.OperateExit(Rigidbody2D _rb2d, GameObject _target, float _speed)
    {

    }
}
