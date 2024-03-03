using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7FristTrap : MonoBehaviour
{
    public Transform FirstGrid;
    public float moveSpeed;
    public bool moveGround = false;
    public bool isActive = true;
    public float y;
    private void Update()
    {
        if (moveGround && isActive)
        {
            Vector3 movementDown = Vector2.down * moveSpeed * Time.deltaTime;
            FirstGrid.Translate(movementDown, Space.Self);
            if (FirstGrid.position.y <= y)
            {
                moveGround = false;
                isActive = false;
                StartCoroutine(MoveToYPosition());
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveGround = true;
        }

    }
    private IEnumerator MoveToYPosition()
    {
        yield return new WaitForSeconds(3f); // 1ÃÊ ´ë±â
        while (FirstGrid.position.y <= 0)
        {
            Vector3 movementUp = Vector2.up * moveSpeed * Time.deltaTime;
            FirstGrid.Translate(movementUp, Space.Self);
            yield return null;
        }
    }

}
