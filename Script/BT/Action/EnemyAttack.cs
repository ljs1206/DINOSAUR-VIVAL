using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class EnemyAttack : Action
{
	private Collider rayCol;
	[SerializeField]
	private SharedLayerMask groundLayer;
	[SerializeField]
	private SharedLayerMask targetLayer;
	[SerializeField]
	private float attackRange;
	[SerializeField]
	private SharedCollider mouseCol;
	[SerializeField]
	private SharedTransform targetTrm;

	private Animator animator;
	private NavMeshAgent agent;

    public override void OnStart()
	{
		agent = Owner.GetComponent<NavMeshAgent>();
		animator = Owner.GetComponent<Animator>();
    }

	public override TaskStatus OnUpdate()
	{
		animator.SetBool("isEat", false);

        Ray ray = new Ray(Owner.transform.position, Vector3.down);

        rayCol = Physics.RaycastAll(ray, 30, groundLayer.Value)[0].collider;

		try
		{
			if (Physics.OverlapSphere(Physics.RaycastAll(ray, 100, groundLayer.Value)[0].point,
				attackRange * Owner.transform.localScale.x, targetLayer.Value)[0])
			{

			}
		}
		catch (System.IndexOutOfRangeException ie)
		{
			animator.SetBool("isAttack", false);
            agent.isStopped = false;
            return TaskStatus.Failure;
		}

        if (Physics.OverlapSphere(Physics.RaycastAll(ray, 100, groundLayer.Value)[0].point,
			attackRange * Owner.transform.localScale.x, targetLayer.Value)[0])
		{
            agent.isStopped = true;
			agent.velocity = Vector3.zero;

			animator.SetBool("isAttack", true);
			animator.SetBool("isMove", false);

            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
	}

    public override void OnDrawGizmos()
    {
        Ray ray = new Ray(Owner.transform.position, Vector3.down);

        Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(Physics.RaycastAll(ray, 100, groundLayer.Value)[0].point, attackRange * Owner.transform.localScale.x);
    }
}