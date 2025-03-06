using System;
using BehaviorDesigner.Runtime;
using Parkjung2016;
using System.Collections;
using System.Collections.Generic;
using MeatStat;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;
public class EnemyHit : MonoBehaviour, IApplyDamage
{
    private BehaviorTree bt;
    private LEnemyAI enemyAI;
    private LFeldOfView lFeldOfView;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float hp;
    private float curretHp;

    private DinoControll dinoControll;
    private int age;
    private WeaponCollider weaponCollider;
    private bool start;
    private int spawnCount;
    private void Awake()
    {
        start = true;
        weaponCollider = GetComponentInChildren<WeaponCollider>();
        dinoControll = GetComponentInChildren<DinoControll>();
        bt = GetComponent<BehaviorTree>();
        enemyAI = GetComponent<LEnemyAI>();
        lFeldOfView = GetComponent<LFeldOfView>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.stoppingDistance = transform.localScale.x * 0.7f;
    }

    private void OnEnable()
    {

        StartCoroutine(EnableCoroutine());
    }
    private void Start()
    {
        meatCount();
    }

    private IEnumerator EnableCoroutine()
    {
        curretHp = hp;

        yield return YieldCache.WaitUntil(() => (((GameScene)SceneManagement.Instance.CurrentScene).Player) != null);

        int currentPlayerLv = ((GameScene)SceneManagement.Instance.CurrentScene).Player.CurrentLevel;
        int maxAge = Mathf.Min(currentPlayerLv + 5, Data.DataMap.Count);
        int minAge = Mathf.Max(currentPlayerLv - 5, 1);

        age = Random.Range(minAge, maxAge);

        Data data = Data.DataMap[age];
        weaponCollider.Power = data.Damage;
        hp = data.Helth;


        if (start)
            Invoke("SetAge", 1);
        else
            SetAge();
        EnableWeaponCollider(0);
    }

    private void SetAge()
    {
        dinoControll.SetAge(age);
        start = false;
    }


    public void HitEnd()
    {
        dinoControll.SwitchEyeShape(0);
        animator.SetBool("isHit", false);
        bt.enabled = true;
        enemyAI.enabled = true;
        lFeldOfView.enabled = true;
    }

    public void ApplyDamage(float damage)
    {
        if (navMeshAgent.baseOffset <= 0.3f)
        {
            dinoControll.SwitchEyeShape(5);
            curretHp -= damage;
            animator.SetBool("isAttack", false);
            animator.SetBool("isHit", true);
            bt.SetVariableValue("Hit", true);
            bt.enabled = false;
            enemyAI.enabled = false;
            lFeldOfView.enabled = false;

            if (curretHp <= 0)
            {
                animator.SetBool("isHit", false);
                curretHp = 0;
                Die();
            }
        }
    }


    public void EnableWeaponCollider(int enable)
    {
        weaponCollider.EnableCollider(Convert.ToBoolean(enable));
    }

    private void Die()
    {
        dinoControll.SwitchEyeShape(6);

        bt.enabled = false;
        enemyAI.enabled = false;
        lFeldOfView.enabled = false;
        navMeshAgent.enabled = false;

        animator.SetBool("isDie", true);
        Invoke(nameof(dropMeat), 0.8f);
    }


    public void DieFalse()
    {
        animator.SetBool("isDie", false);
    }
    private void meatCount()
    {
        if (age <= 5)
        {
            spawnCount = 1;
        }
        else
        {
            spawnCount = (int)age / 5 + 1;
        }
    }
    private void dropMeat()
    {
        for (int i = 0; i < spawnCount; ++i)
            PoolManagerq.SpawnFromPool("Meat", transform.position, Quaternion.identity);


        GameObject[] gm = GameObject.FindGameObjectsWithTag("Meat");
        foreach (var dd in gm)
        {
            RetuenPool pool = dd.GetComponent<RetuenPool>();
            pool.Spawn();
        }

        DG.Tweening.Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(-50, 10)).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}