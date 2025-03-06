using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestLanding : MonoBehaviour
{
    [SerializeField]
    private SharedBool landing;
    [SerializeField]
    private Transform target;

    private NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        agent.SetDestination(target.position);
        if (agent.baseOffset > 0)
        {
            agent.baseOffset -= Time.deltaTime * 5f;
        }
        return;
    }
}
