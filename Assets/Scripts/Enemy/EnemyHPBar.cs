using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] public Transform TargetTransform;
    [SerializeField] public EnemyBase TargetEnemyBase;
    [SerializeField] private Slider EnemyHPSlider;
    private Camera _mainCamera;

    public Vector3 _offset = new Vector3(0, 1.5f, 0);

    void Awake()
    {
        EnemyHPSlider = GetComponent<Slider>();
        _mainCamera = Camera.main;
    }

    public void Setup(EnemyBase enemyBase)
    {
        TargetEnemyBase = enemyBase;
        TargetTransform = enemyBase.transform;
        EnemyHPSlider.maxValue = TargetEnemyBase._EnemyMAXHP;
        EnemyHPSlider.value = TargetEnemyBase._EnemyHP;
        gameObject.SetActive(true);
    }

    void LateUpdate() 
    {
        if (TargetTransform == null || !TargetEnemyBase.gameObject.activeInHierarchy)
        {
            ReturnToPool();
            return;
        }

        Vector3 worldPos = TargetTransform.position + _offset;
        transform.position = _mainCamera.WorldToScreenPoint(worldPos);
        float currentHP = TargetEnemyBase._EnemyHP;
        float maxHP = TargetEnemyBase._EnemyMAXHP;

        EnemyHPSlider.value = currentHP;

        if (currentHP >= maxHP)
        {
            EnemyHPSlider.gameObject.SetActive(false);
        }
        else
        {
            EnemyHPSlider.gameObject.SetActive(true);
        }
    }

    public void ReturnToPool()
    {
        TargetTransform = null;
        TargetEnemyBase = null;
        gameObject.SetActive(false);
    }
}
