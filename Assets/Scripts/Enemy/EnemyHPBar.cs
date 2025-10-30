using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    private Camera _MainCamera;
    private EnemyBase _enemyBase;
    private Slider _enemyHpSlider;
    private float MaxHP;

    void Awake()
    {
        _enemyBase = GetComponentInParent<EnemyBase>();
        _enemyHpSlider = GetComponent<Slider>();
        MaxHP = _enemyBase._EnemyMAXHP;
    }

    void Start()
    {
        _MainCamera = Camera.main;
        _enemyHpSlider.gameObject.SetActive(false);
        if (_enemyHpSlider != null && _enemyBase != null)
        {
            _enemyHpSlider.maxValue = MaxHP;
        }
    }
    void Update()
    {
        transform.rotation = _MainCamera.transform.rotation;
        if (_enemyHpSlider.value < MaxHP)
            _enemyHpSlider.gameObject.SetActive(true);
        else
            _enemyHpSlider.gameObject.SetActive(false);
        _enemyHpSlider.value = _enemyBase._EnemyHP;
    }
}
