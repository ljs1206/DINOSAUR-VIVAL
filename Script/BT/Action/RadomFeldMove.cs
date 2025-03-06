using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using FIMSpace.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RadomFeldMove : Action
{
    [SerializeField] private float radius;
    [SerializeField] private SharedTransform originPos;
    [SerializeField] private float enemyOriginY;

    private float angle;
    private NavMeshAgent navMeshAgent;
    private Vector3 destination;
    private Animator animator;
    private BehaviorTree bt;
    private bool check;

    public override void OnStart()
    {
        check = false;
        navMeshAgent = Owner.GetComponent<NavMeshAgent>();
        animator = Owner.GetComponent<Animator>();
        bt = Owner.GetComponent<BehaviorTree>();

        navMeshAgent.speed = 4;
        navMeshAgent.stoppingDistance = 0;
        bt.SetVariableValue("landing", false);
        bt.SetVariableValue("Hit", false);
        animator.SetBool("isMove", false);

        FeldMove();
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        yield return YieldCache.WaitForSeconds(.5f);
        check = true;
    }

    public override void OnEnd()
    {
        navMeshAgent.stoppingDistance = 2;
    }

    public override TaskStatus OnUpdate()
    {
        if (!check || !navMeshAgent.enabled) return TaskStatus.Running;
        if (navMeshAgent.baseOffset < enemyOriginY)
        {
            navMeshAgent.baseOffset += Time.deltaTime * 7f;
        }

        animator.SetBool("isEat", false);
        animator.SetBool("isFly", true);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        return navMeshAgent.velocity.sqrMagnitude <= .1f ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(originPos.Value.position.x,
            Owner.transform.position.y, originPos.Value.position.z), radius);
    }

    private void FeldMove()
    {
        animator.SetBool("isAttack", false);

        if (!navMeshAgent.enabled) return;
        navMeshAgent.isStopped = false;

        angle = Random.Range(0, 361);

        // originPos.Value.position = new Vector3(originPos.Value.position.x, 0, originPos.Value.position.z);

        destination = originPos.Value.position + new Vector3(Mathf.Cos(angle) * radius,
            transform.position.y, Mathf.Sin(angle) * radius);

        navMeshAgent.SetDestination(GetRandomPositionOnNavMesh(radius));

        // Owner.transform.position = new Vector3(transform.position.x, enemyOriginY, Owner.transform.position.z);
    }

    Vector3 GetRandomPositionOnNavMesh(float range)
    {
        NavMeshHit hit;
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * range;
            randomDirection += originPos.Value.position;
            if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return Owner.transform.position;
    }
}