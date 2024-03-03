using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4GroundDown : MonoBehaviour
{
    public Transform GroundDown;
    public bool isGround = false;
    public float moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGround)
        {
            Vector3 movement = Vector2.down * moveSpeed * Time.deltaTime;
            GroundDown.Translate(movement, Space.Self);
            if (GroundDown.position.y <= -23f)
            {
                isGround = false;
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
