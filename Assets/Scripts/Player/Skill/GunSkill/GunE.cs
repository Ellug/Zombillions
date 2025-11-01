using UnityEngine;

public class GunE : SkillBase
{
    private GameObject _gravityBulletPrefab;
    private float _range = 60f;
    private float _dmg = 40f;

    public GunE()
    {
        _skillName = "Gravity Bullet";
        _coolTime = 10f;
        _soundIndex = 2;
        _icon = Resources.Load<Sprite>("Icons/Skill_GunE");
        _gravityBulletPrefab = Resources.Load<GameObject>("Prefabs/GravityBullet");
    }
    
    protected override void ActivateSkill()
    {
        if (_player == null) return;

        // 마우스 위치로 발사
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, 1 << 6)) // 6번 레이어마스크 = Ground. 하드코딩 ㅈㅅ 레이어 변경시 수정.
        {
            Debug.Log($"3 스킬 {_skillName} 실행");

            // 방향 계산
            Vector3 dir = (hit.point - _player.transform.position).normalized;
            Vector3 spawnPos = _player.transform.position + dir * 0.5f;

            // 거리 계산
            float distance = Vector3.Distance(_player.transform.position, hit.point);
            float finalRange = Mathf.Min(distance, _range);

            // 생성
            GameObject bulletObj = Object.Instantiate(_gravityBulletPrefab, spawnPos, Quaternion.LookRotation(dir));
            var bullet = bulletObj.GetComponent<GravityBullet>();
            bullet.Init(dir, _dmg, finalRange);
        }

    }
}
