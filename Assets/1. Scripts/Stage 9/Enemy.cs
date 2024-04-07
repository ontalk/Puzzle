using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target; // 따라갈 대상의 Transform
    public float minDistance = 10f; // 타겟과의 최소 거리
    public float moveSpeedNormal = 5f; // 일반 이동 속도
    public float moveSpeedClose = 3f; // 타겟과 가까울 때의 이동 속도

    private float moveSpeed; // 현재 이동 속도

    void Start()
    {
        // 초기 이동 속도 설정
        moveSpeed = moveSpeedNormal;
    }

    void Update()
    {
        if (target != null) // target이 null이 아닌 경우에만 실행
        {
            // 타겟과의 거리 계산
            float distanceToTarget = Mathf.Abs(target.position.x - transform.position.x);

            // 거리에 따라 이동 속도 조절
            if (distanceToTarget < minDistance)
            {
                moveSpeed = moveSpeedClose; // 타겟과 가까울 때의 이동 속도 적용
            }
            else
            {
                moveSpeed = moveSpeedNormal; // 일반 이동 속도 적용
            }

            // Enemy가 target을 향해 이동하도록 설정
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
 