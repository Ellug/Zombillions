using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;     // 이동 속도
    public float rotateSpeed = 10f;  // 회전 속도

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A, D
        float v = Input.GetAxisRaw("Vertical");   // W, S

        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        if (moveDir != Vector3.zero)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
