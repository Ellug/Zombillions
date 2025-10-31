using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    [SerializeField] private Transform _towerHead;
    [SerializeField] private float _rotationSpeed;
    private List<Transform> _targetEnemies = new List<Transform>();
    private Transform currentEnemy;

    private void Update()
    {
        RotationUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!_targetEnemies.Contains(other.transform))
            {
                _targetEnemies.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_targetEnemies.Contains(other.transform))
            {
                _targetEnemies.Remove(other.transform);
            }
        }
    }
    public void CheckEmpty()
    {
        _targetEnemies.RemoveAll(emptyEnemy => emptyEnemy == null || !emptyEnemy.gameObject.activeSelf);
    }

    public Transform GetCurrentEnemy()
    {
        CheckEmpty();

        if (_targetEnemies.Count > 0)
        {
            return _targetEnemies[0];
        }
        else
        {
            return null;
        }
    }

    private void RotationUpdate()
    {
        if(_towerHead == null)
        {
            return;
        }
        currentEnemy = GetCurrentEnemy();
        if (currentEnemy != null)
        {
            Vector3 direction = currentEnemy.position - _towerHead.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                _towerHead.rotation = Quaternion.Lerp
                    (_towerHead.rotation,
                    rotation,
                    Time.deltaTime * _rotationSpeed
                    );
            }
        }
        else
        {
            _towerHead.rotation = Quaternion.Lerp
                    (_towerHead.rotation,
                    Quaternion.LookRotation(Vector3.forward),
                    Time.deltaTime * _rotationSpeed
                    );
        }
    }
}
