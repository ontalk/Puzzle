using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class TranslateGround : MonoBehaviour
{
    public Transform Ground;
    public float moveSpeed = 1f;
    public bool isGround = false;
    public float groundStopX = 27.31f; // x�࿡�� ���ߴ� ��ġ
    public AudioClip TranformGroundClip;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isGround)
        {
            Vector3 movement = Vector2.right * moveSpeed * Time.deltaTime;
            Ground.Translate(movement, Space.Self);

            // ���� ���� x ��ġ�� ���ߴ� ��ġ�� �����ߴ��� Ȯ��
            if (Ground.localPosition.x >= groundStopX)
            {
                isGround = false; // �������� ����
            }
        }

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isGround = true;
        }
    }
}
