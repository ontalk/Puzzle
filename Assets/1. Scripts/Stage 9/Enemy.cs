using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target; // ���� ����� Transform
    public float minDistance = 10f; // Ÿ�ٰ��� �ּ� �Ÿ�
    public float moveSpeedNormal = 5f; // �Ϲ� �̵� �ӵ�
    public float moveSpeedClose = 3f; // Ÿ�ٰ� ����� ���� �̵� �ӵ�

    private float moveSpeed; // ���� �̵� �ӵ�

    void Start()
    {
        // �ʱ� �̵� �ӵ� ����
        moveSpeed = moveSpeedNormal;
    }

    void Update()
    {
        if (target != null) // target�� null�� �ƴ� ��쿡�� ����
        {
            // Ÿ�ٰ��� �Ÿ� ���
            float distanceToTarget = Mathf.Abs(target.position.x - transform.position.x);

            // �Ÿ��� ���� �̵� �ӵ� ����
            if (distanceToTarget < minDistance)
            {
                moveSpeed = moveSpeedClose; // Ÿ�ٰ� ����� ���� �̵� �ӵ� ����
            }
            else
            {
                moveSpeed = moveSpeedNormal; // �Ϲ� �̵� �ӵ� ����
            }

            // Enemy�� target�� ���� �̵��ϵ��� ����
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
 