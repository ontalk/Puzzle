using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7SecondTrap : MonoBehaviour
{
    public Transform Ground;
    public float Speed;
    public bool moveGround = false;
    private bool isActive = true;
    private bool isSecondActive = false;
    public float FirstY;
    public float SecondX;
    public float ThirdY;
    public float y;
    public Transform FirstGrid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveGround && isActive)
        {
            Vector3 movementDown = Vector2.up * Speed * Time.deltaTime;
            Ground.Translate(movementDown, Space.Self);
            if (Ground.position.y >= FirstY)
            {
                moveGround = false;
                isActive = false;
                StartCoroutine(LeftXPosition());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            moveGround = true;
        }
    }
    private IEnumerator LeftXPosition()
    {
        yield return new WaitForSeconds(0.25f); // 1초 대기
        while (Ground.position.x >= SecondX)
        {
            // 왼쪽으로 이동
            Vector3 movementLeft = Vector2.left * Speed * Time.deltaTime;
            Ground.Translate(movementLeft, Space.Self);

            // 한 프레임을 기다림
            yield return null;
        }
        StartCoroutine(DownGround());
        StartCoroutine(DownPosition());

    }
    private IEnumerator DownPosition() //원래 위치로 돌아가기
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        while (Ground.position.y >= ThirdY)
        {
            Vector3 movementUp = Vector2.down * Speed * Time.deltaTime;
            Ground.Translate(movementUp, Space.Self);
            yield return null;
        }
    }
    private IEnumerator DownGround() //함정 땅 움직이기
    {
        yield return new WaitForSeconds(0.25f);
        
        while (FirstGrid.position.y >= y)
        {
            Vector3 movementDown = Vector2.down * Speed * Time.deltaTime;
            FirstGrid.Translate(movementDown, Space.Self);
            isSecondActive = true;
            StartCoroutine(MoveToYPosition());
        }
    }
    private IEnumerator MoveToYPosition()
    {
        yield return new WaitForSeconds(2f); // 1초 대기
        while (FirstGrid.position.y <= 0)
        {
            Vector3 movementUp = Vector2.up * Speed * Time.deltaTime;
            FirstGrid.Translate(movementUp, Space.Self);
            yield return null;
        }
    }
}
