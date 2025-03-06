using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Eating : Action
{
	[SerializeField]
	private SharedCollider mouseCol;
	private Animator animator;
	private NavMeshAgent agent;

	public override void OnStart()
	{
		animator = Owner.GetComponent<Animator>();
		agent = Owner.GetComponent<NavMeshAgent>();
    }

	public override TaskStatus OnUpdate()
	{
		animator.SetBool("isMove", false);
		animator.SetBool("isAttack", false);
		animator.SetBool("isEat", true);

        agent.isStopped = true;
		agent.velocity = Vector3.zero;

        return TaskStatus.Success;
	}
}