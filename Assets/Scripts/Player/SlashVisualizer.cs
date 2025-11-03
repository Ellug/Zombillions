using UnityEngine;

public static class SlashVisualizer
{
    /// <param name="center">중심 좌표</param>
    /// <param name="forward">기준 방향</param>
    /// <param name="radius">부채꼴 반경</param>
    /// <param name="angle">부채꼴 각도</param>
    /// <param name="duration">화면에 유지되는 시간(초)</param>
    /// <param name="color">라인 색상</param>
    /// <param name="startWidth">시작 두께</param>
    /// <param name="endWidth">끝 두께</param>
    public static void DrawArc( Vector3 center, Vector3 forward, float radius, float angle, float duration = 0.2f, Color? color = null, float startWidth = 0.01f, float endWidth = 0.5f, float heightOffset = 0.5f )
    {
        GameObject arcObj = new GameObject("DebugArc");
        var lr = arcObj.AddComponent<LineRenderer>();

        lr.positionCount = 20; // 점 개수 (부드러움 조절)
        lr.startWidth = startWidth;
        lr.endWidth = endWidth;
        
        lr.material = new Material(Shader.Find("Unlit/Color"));
        lr.material.color = color ?? Color.white; // 기본값 흰색

        // 중심 기준 부채꼴 좌표 계산
        int count = lr.positionCount;
        float startAngle = -angle * 0.5f;
        // y축
        Vector3 finalCenter = center + Vector3.up * heightOffset;

        for (int i = 0; i < count; i++)
        {
            float curAngle = startAngle + (angle / (count - 1)) * i;
            Vector3 dir = Quaternion.Euler(0, curAngle, 0) * forward;
            lr.SetPosition(i, finalCenter + dir * radius);
        }

        Object.Destroy(arcObj, duration);
    }
}
