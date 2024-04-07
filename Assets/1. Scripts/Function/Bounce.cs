using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �÷��̾����� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� Rigidbody ������Ʈ�� ������
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // �÷��̾�� ���� ���� ƨ��� ȿ���� ��
                playerRigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
            }
        }
    }
}
