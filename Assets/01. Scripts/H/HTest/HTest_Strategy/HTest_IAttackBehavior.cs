using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HTest_IAttackBehavior
{
    void OperateEnter();
    void OperateUpdate();
    void OperateExit();
}

class HTest_IAttackBehavior_FireProjectile : HTest_IAttackBehavior
{
    public void OperateEnter()
    {
        throw new System.NotImplementedException();
    }
    public void OperateExit()
    {
        throw new System.NotImplementedException();
    }

    public void OperateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
