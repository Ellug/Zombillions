using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    [SerializeField] private Transform _towerHead;
    [SerializeField] private float _rotationSpeed;
    private TowerData _towerData;
    private Transform _currentEnemy;
    private bool _OnTrigger = false;

    private void Update()
    {
        //SetCurrentEnemy();
        if (_OnTrigger == true)
        {
            SetCurrentEnemy();
        }
        RotationUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _currentEnemy)
        {
            return;
        }
        else if (other.transform != _currentEnemy && other.CompareTag("Enemy"))
        {
            _OnTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == _currentEnemy)
        {
            _currentEnemy = null;
        }
    }
    public void SetTowerData(TowerData data)
    {
        _towerData = data;
    }

    public void SetCurrentEnemy()
    {
        if (_currentEnemy != null && _currentEnemy.gameObject.activeSelf)
        {
            return;
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, _towerData.attackRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy") && hit.gameObject.activeSelf)
            {
                _currentEnemy = hit.transform;
                return;
            }
        }
        _OnTrigger = false;
        _currentEnemy = null;
    }

    private void RotationUpdate()
    {
        if (_towerHead == null)
        {
            return;
        }

        if (_currentEnemy != null)
        {
            Vector3 direction = _currentEnemy.position - _towerHead.position;
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
    public Transform GetCurrentEnemy()
    {
        return _currentEnemy;
    }
}
