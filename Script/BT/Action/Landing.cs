using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using UnityEditor.Rendering;

public class Landing : Action
{
	[SerializeField]
	private SharedBool landing;
	[SerializeField]
	private SharedTransform target;

	private NavMeshAgent agent;

	public override void OnStart()
	{
        agent = Owner.GetComponent<NavMeshAgent>();
    }

	public override TaskStatus OnUpdate()
	{
		agent.isStopped = false;

        if (!landing.Value)
		{
			agent.SetDestination(target.Value.position);
			if(agent.baseOffset > 0)
			{
				agent.baseOffset -= Time.deltaTime * 5f;
			}
			return TaskStatus.Running;
		}

		return TaskStatus.Success;
	}
}