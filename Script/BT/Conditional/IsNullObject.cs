using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

public class IsNullObject : Conditional
{
    #region serializeField
    [SerializeField]
    private SharedVariable obj;
    [SerializeField]
    private bool isNull;
    #endregion

    public override TaskStatus OnUpdate()
    {
        if (isNull)
        {
            return ReferenceEquals(obj.GetValue(), null) ? TaskStatus.Success : TaskStatus.Failure;
        }
        else
        {
            return !ReferenceEquals(obj.GetValue(), null) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}