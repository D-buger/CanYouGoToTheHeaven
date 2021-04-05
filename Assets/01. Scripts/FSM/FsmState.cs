using System.Collections.Generic;
using UnityEngine;

public abstract class FsmState<T>
{
    abstract public void Enter(T target);
    abstract public void Exit(T target);
    abstract public void HandleInput(T target);
    abstract public void Update(T target);
    abstract public void FixedUpdate(T target);
}
