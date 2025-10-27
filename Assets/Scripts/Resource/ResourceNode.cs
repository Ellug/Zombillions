using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [Tooltip("±¤¸Æ Ã¼·Â")]
    [SerializeField] private int maxHP = 20;

    [Tooltip("±¤¸Æ ¼ÒÁø½Ã °ñµå º¸»ó")]
    [SerializeField] private int goldOnDeplete = 10;
    [SerializeField][Range(30f, 120f)] private float respawnTime = 30f;
    [SerializeField] private Collider hitCollider;


    [SerializeField] private GoldSink goldSink;
    //private IGoldSink _goldSink;
    

    //·±Å¸ÀÓ »óÅÂ
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
        // °ñµå Áö±Þ
        goldSink.Gain(goldOnDeplete); 

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
