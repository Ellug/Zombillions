using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Scriptble Object/TowerData")]
public class TowerData : ScriptableObject
{
    public enum TowerTag
    {
        HQTower, AttackTower, DefanceTower, TriggerTower
    }

    public TowerTag towerTag;
    public float maxHp;
    public Vector3 size;
    public float attackPower;
    public float attackRange;
    public float attackSpeed;
    public int cost;
}
