using BehaviorDesigner.Runtime;
using FIMSpace.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LFeldOfView : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;
    public LayerMask targetLayer;
    [SerializeField]
    private float radius;

    private BehaviorTree bt;
    private Animator animator;

    private void Awake()
    {
        bt = GetComponent<BehaviorTree>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, 30, groundLayer))
        {

            if (Physics.OverlapSphere(Physics.RaycastAll(ray, 30, groundLayer)[0].point, radius, targetLayer).Length > 0)
            {
                if (animator.GetBool("isAttack") || animator.GetBool("isEat"))
                {
                    Vector3 dir = transform.position - Physics.OverlapSphere(Physics.RaycastAll(ray, 30, groundLayer)[0].point,
                        radius, targetLayer)[0].transform.position;

                    Quaternion targetRotate = Quaternion.LookRotation(dir, Vector3.up);

                    Vector3 eulerTargetRotate = targetRotate.eulerAngles;

                    eulerTargetRotate.x = 0;
                    eulerTargetRotate.y += 180f;
                    eulerTargetRotate.z = 0;

                    transform.rotation = Quaternion.Euler(eulerTargetRotate);

                    //Quaternion rot = Quaternion.AngleAxis(angle, -transform.up);
                    //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 4 * Time.deltaTime);
                }
                
                bt.SetVariableValue("target",
                Physics.OverlapSphere
                (Physics.RaycastAll(ray, 30, groundLayer)[0].point, radius, targetLayer)[0].transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        try
        {
            Gizmos.DrawWireSphere(Physics.RaycastAll(new Ray(transform.position, Vector3.down), 30, groundLayer)[0].point, radius);
        }
        catch(IndexOutOfRangeException ie)
        {
            return;
        }

        Gizmos.DrawWireSphere(Physics.RaycastAll(new Ray(transform.position, Vector3.down), 30, groundLayer)[0].point, radius);
    }
}
