using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ÿ�� ���� ó��
public class Tower : MonoBehaviour
{
    [SerializeField] private TowerData towerData; 

    private float _towerCurrentHp;
    public float TowerCurrentHp => _towerCurrentHp;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        if(towerData == null)
        {
            return;
        }

        //�ش� ������Ʈ �±� ����
        string tagName = towerData.towerTag.ToString();
        gameObject.tag = tagName;

        _towerCurrentHp = towerData.maxHp;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        TakeDamage(2f); //������ Ȯ�ο� �ڵ�
    }

    public void TakeDamage(float damage)
    {
        _towerCurrentHp -= damage;

        Debug.Log($"Ÿ�� ü���� {_towerCurrentHp}�� �Ǿ����ϴ�."); //������ Ȯ�ο� �ڵ�

        if (_towerCurrentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
