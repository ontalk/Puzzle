using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage11Down : MonoBehaviour
{
    public Transform GroundDown;
    public float y;
    public bool isGround = false;
    public float moveSpeed;
    private bool isActive = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGround && !isActive)
        {
            Vector3 movement = Vector2.down * moveSpeed * Time.deltaTime;
            GroundDown.Translate(movement, Space.Self);
            if (GroundDown.position.y <= y)
            {
                isGround = false;
                isActive = true;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            isGround = true;
        }
    }
}
