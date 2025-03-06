using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct LInfo
{
    public int hp;
    public int power;
    public int speed;
    public float attackCoolTime;
    public float orbit;
}

[CreateAssetMenu(menuName = "SO/LEnemyInfo")]
public class LEnemyInfo : ScriptableObject
{
    public List<LInfo> lInfos = new();
}
