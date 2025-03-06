using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using FIMSpace.Basics;

public class MoveTo : Action
{
	[SerializeField]
	private SharedBool isFindPlayer;
	[SerializeField]
	private SharedTransform targetTrm;
	[SerializeField]
	private SharedBool moveState;
	[SerializeField]
	private SharedTransform originTrm;

	private NavMeshAgent agent;
	private Animator animator;

	public override void OnStart()
	{
		agent = Owner.GetComponent<NavMeshAgent>();
		animator = Owner.GetComponent<Animator>();
		moveState = false;
    }

	public override TaskStatus OnUpdate()
	{
		if (isFindPlayer.Value)
        {
			animator.SetBool("isFly", false);
            animator.SetBool("isAttack", false);
			animator.SetBool("isEat", false);
			animator.SetBool("isMove", true);
			
			agent.speed = 6;
			agent.SetDestination(targetTrm.Value.position);

            agent.isStopped = false;

            return TaskStatus.Success;
        }
		
		if(agent.velocity.magnitude <= 0 || agent.destination == targetTrm.Value.position)
		{
			agent.SetDestination(originTrm.Value.position);

            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
	}
}