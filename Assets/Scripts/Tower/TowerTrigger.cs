using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    [SerializeField] private Transform _towerHead;
    [SerializeField] private float _rotationSpeed;
    //private Transform _targetEnemy;
    private List<Transform> _targetEnemies = new List<Transform>();

    private void Update()
    {
        RotationUpdate();

        //테스트용
        if (Input.GetKeyDown(KeyCode.A))
        {
            Transform currentEnemy = GetCurrentEnemy();
            RemoveEnemy(currentEnemy);
        }
    }

    public Transform GetCurrentEnemy()
    {
        if (_targetEnemies.Count > 0)
        {
            return _targetEnemies[0];
        }
        return null;
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
    private void RotationUpdate()
    {
        if(_towerHead == null)
        {
            return;
        }

        Transform currentEnemy = GetCurrentEnemy();
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
    public void RemoveEnemy(Transform enemy)
    {
        if (_targetEnemies.Contains(enemy))
        {
            _targetEnemies.Remove(enemy);
        }
    }
}
