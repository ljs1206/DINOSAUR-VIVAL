using UnityEngine;
using DG.Tweening;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using System;
using JetBrains.Annotations;

public class LEnemyAI : MonoBehaviour
{
    [SerializeField] private LEnemyInfo lEnemyInfo;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float range;
    [SerializeField] private LayerMask targetLayer;

    private Animator animator;
    private Rigidbody rg;
    private BehaviorTree bt;
    private NavMeshAgent navMeshAgent;
    private LFeldOfView lFeldOfView;

    public LayerMask[] layers;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rg = GetComponent<Rigidbody>();
        bt = GetComponent<BehaviorTree>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        lFeldOfView = GetComponent<LFeldOfView>();
    }

    private void Start()
    {
        // transform.localPosition = new Vector3(transform.localPosition.x,
        //     transform.localPosition.y + lEnemyInfo.lInfos[0].orbit,transform.localPosition.z);
        animator.SetBool("isFly", true);
    }

    private void Update()
    {
        if (!(bool)bt.GetVariable("Hit").GetValue())
        {
            bt.SetVariableValue("targetLayer", layers[0]);
            targetLayer = layers[0];
            lFeldOfView.targetLayer = layers[0];
        }
        else
        {
            bt.SetVariableValue("targetLayer", layers[1]);
            targetLayer = layers[1];
            lFeldOfView.targetLayer = layers[1];
            animator.SetBool("isEat", false);
        }

        Ray ray = new Ray(transform.position, Vector3.down);


        if (Physics.Raycast(ray, 30, groundLayer))
        {
            if (navMeshAgent.baseOffset <= 0.3f)
            {
                animator.SetBool("isFly", false);
                bt.SetVariableValue("landing", true);
            }

            try
            {
                if (Physics.OverlapSphere(Physics.RaycastAll(ray, 30, groundLayer)[0].point, range, targetLayer)[0])
                {
                }
            }
            catch (IndexOutOfRangeException ie)
            {
                bt.SetVariableValue("IsFindPlayer", false);
                return;
            }

            if (Physics.OverlapSphere(Physics.RaycastAll(ray, 30, groundLayer)[0].point, range, targetLayer)[0])
            {
                bt.SetVariableValue("IsFindPlayer", true);
            }
        }
    }

    public void EatAinmeEvent()
    {
        ((Transform)bt.GetVariable("target").GetValue()).GetComponent<Meat>().BeEaten(transform);
    }
}