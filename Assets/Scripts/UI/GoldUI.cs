using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUI Ÿ���� ����Ϸ��� �ݵ�� �ʿ�

/// <summary>
/// ȭ�鿡 ǥ���� ���� ���� �ڿ� UI
/// </summary>
public class GoldUI : MonoBehaviour // ���� GameObject �� �ٿ� �����ϴ� ������Ʈ
{
    [SerializeField] private TextMeshProUGUI goldText;
    // �ν����Ϳ��� �Ҵ��� �� �ֵ��� ����ȭ
    // ��Ÿ�ӿ� �� ������ �ؽ�Ʈ ���� ����

    private void HandleGoldChanged(int value)
    {
        if(goldText == null)
        {
            return;
        }

        goldText.text = value.ToString();
    }
    // ��� ���� ����� �� ȣ��Ǵ� �ݹ� �Լ�
    // GoldManager.OnGoldChanged �̺�Ʈ�� ���� ȣ��

    private void OnEnable()
    {
        if(GoldManager.Instance != null)
        {
            GoldManager.Instance.OnGoldChanged += HandleGoldChanged;
        }
    }
    // ������Ʈ�� Ȱ��ȭ �ɶ� GoldManager �̺�Ʈ ����
    // ���� ��尡 �ٲ�� HandleGoldChanged �� �ڵ� ȣ��ȴ�

    private void OnDisable()
    {
        if(GoldManager.Instance != null)
        {
            GoldManager.Instance.OnGoldChanged -= HandleGoldChanged;
        }
    }
    // ������Ʈ ��Ȱ��ȭ �ɶ� ȣ��
    // �ߺ� ���� , �޸� ���� ���� ���� ����

    void Start()
    {
        if(GameManager.Instance != null)
        {
            HandleGoldChanged(GoldManager.Instance.CurrentGold);
        }
    }
    //���� ���۵� �� �̺�Ʈ�� �߻����� �ʾҴ��� ���� ��� ���� �ѹ� ǥ��
}
