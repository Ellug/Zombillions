using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [Tooltip("광맥 체력")]
    [SerializeField] private int maxHP = 20;

    [Tooltip("광맥 소진시 골드 보상")]
    [SerializeField] private int goldOnDeplete = 10;
    [SerializeField][Range(30f, 120f)] private float respawnTime = 30f;
    [SerializeField] private Collider hitCollider;

    //런타임 상태
    private int currentHP;
    private bool active = true;
    private Coroutine respawnCo;

    void Awake()
    {
        if(hitCollider == null)
        {
            hitCollider = GetComponent<Collider>();
        }
    }

    private void OnEnable()
    {
        Activate();
    }

    private void OnDisable()
    {
        if(respawnCo != null)
        {
            StopCoroutine(respawnCo);
            respawnCo = null;
        }
    }

    public void TakenDamage(int damage)
    {
        if(!active)
        {
            return;
        }
        if(damage <= 0)
        {
            return;
        }

        currentHP = Mathf.Max(0, currentHP - damage);

        if(currentHP == 0)
        {
            Deplated();
        }
    }

    private void Activate()
    {
        active = true;
        currentHP = maxHP;
        if(hitCollider != null)
        {
            hitCollider.enabled = true;
        }
    }

    private void Deactivate()
    {
        active = false;
        if(hitCollider != null)
        {
            hitCollider.enabled = false;
        }
    }

    private void Deplated()
    {
        // 골드 지급
        //ResourceManager.Instance.Earn(goldOnDeplete); // 매니저 이름은 임시 작성

        Deactivate();

        if(respawnCo != null)
        {
            StopCoroutine(respawnCo);
        }

        respawnCo = StartCoroutine(RespawnRoutine(respawnTime));
    }

    private IEnumerator RespawnRoutine(float seconds)
    {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Activate();
    }
}
