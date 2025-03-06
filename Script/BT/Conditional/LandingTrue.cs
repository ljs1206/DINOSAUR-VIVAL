using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LandingTrue : Conditional
{
	[SerializeField]
	private SharedBool landing;

	public override TaskStatus OnUpdate()
	{
		if (landing.Value)
		{
			return TaskStatus.Success;
		}

		return TaskStatus.Failure;
	}
}