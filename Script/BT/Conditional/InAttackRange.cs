using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using System;

namespace LJS
{
    public class InAttackRange : Conditional
    {
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private SharedLayerMask targetLayer;
        private NavMeshAgent navMesh;

        public override void OnStart()
        {
            navMesh = Owner.GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit[] hits = Physics.RaycastAll(ray, 30, groundLayer);
            if (hits.Length > 0)
            {
                if (Physics.OverlapSphere(hits[0].point, attackRange,
                        targetLayer.Value).Length <= 0)
                {
                    return TaskStatus.Failure;
                }
                else
                {

                    return TaskStatus.Success;
                }
            }
            else return TaskStatus.Failure;

        }
    }
}