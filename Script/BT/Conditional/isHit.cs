using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class isHit : Conditional
{
	[SerializeField]
	private SharedBool _isHit;
	[SerializeField]
	private bool TandF;

	public override TaskStatus OnUpdate()
	{
		if (_isHit.Value == TandF)
		{
            return TaskStatus.Success;
        }
		return TaskStatus.Failure;
	}
}