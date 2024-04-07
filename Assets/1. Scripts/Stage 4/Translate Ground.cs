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
    public float groundStopX = 27.31f; // x축에서 멈추는 위치
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

            // 땅의 로컬 x 위치가 멈추는 위치에 도달했는지 확인
            if (Ground.localPosition.x >= groundStopX)
            {
                isGround = false; // 움직임을 멈춤
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
