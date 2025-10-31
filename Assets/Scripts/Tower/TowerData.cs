using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Scriptble Object/TowerData")]
public class TowerData : ScriptableObject
{
    public enum TowerTag
    {
        HQTower, AttackTower, DefenceTower, TriggerTower
    }
    [SerializeField] public GameObject _towerPrefab;
    public TowerTag towerTag;
    public float maxHp;
    public Vector3 size;
    public float attackPower;
    public float attackRange;
    public float attackSpeed;
    public float bulletSpeed;
    public int pierce;
    public float knockback;
    public int cost;
}
