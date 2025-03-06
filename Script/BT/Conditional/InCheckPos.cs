using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InCheckPos : Conditional
{
    [SerializeField]
    private float checkPosRange;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private SharedLayerMask targetLayer;

    public override TaskStatus OnUpdate()
	{
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.OverlapSphere(Physics.RaycastAll(ray, 30, groundLayer)[0].point, 
                checkPosRange * Owner.transform.localScale.x, targetLayer.Value).Length <= 0)
        {
            return TaskStatus.Failure;
        }

        return TaskStatus.Success;
    }
}