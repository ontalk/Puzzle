using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5GroundUp : MonoBehaviour
{
    public Transform GroundUp;
    public bool isGround = false;
    public float moveSpeed;
    public float y;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGround)
        {
            Vector3 movement = Vector2.up * moveSpeed * Time.deltaTime;
            GroundUp.Translate(movement, Space.Self);
            if (GroundUp.position.y >= y)
            {
                isGround = false;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isGround)
        {
            isGround = true;

        }
    }
}
