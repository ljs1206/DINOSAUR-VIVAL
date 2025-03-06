using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class FindLastPos : Action
{
	[SerializeField]
	private SharedTransform target;
	[SerializeField]
	private SharedBool findPlayer;
	[SerializeField]
	private SharedTransform originPos;

	private NavMeshAgent agent;

	public override void OnStart()
	{
        agent = GetComponent<NavMeshAgent>();

		if(!findPlayer.Value)
		{
			if(agent.remainingDistance <= 0)
			{
				agent.SetDestination(originPos.Value.position);
				return;
			}
            agent.SetDestination(target.Value.position);
        }
    }

    public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}