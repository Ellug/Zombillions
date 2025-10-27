using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTest : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private int damage = 1;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out var hit, range))
            {
                hit.collider.GetComponent<ResourceNode>()?.TakenDamage(damage);
            }
        }
    }
}
