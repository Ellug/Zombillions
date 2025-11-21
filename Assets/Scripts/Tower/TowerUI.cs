using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private Tower _tower;

    private Camera _mainCamera;
    public Slider hpBar;
    private float maxHp;
    private float currentHp;

    private void Start()
    {
        _mainCamera = Camera.main;
        _tower = GetComponentInParent<Tower>();
        maxHp = _tower.TowerMaxHp;
    }
    void LateUpdate()
    {
        transform.rotation = _mainCamera.transform.rotation;
        TowerHpCalc();
    }
    private void TowerHpCalc()
    {
        currentHp = _tower.TowerCurrentHp;
        hpBar.value = currentHp / maxHp;
    }
}
