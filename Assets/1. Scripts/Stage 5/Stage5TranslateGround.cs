using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5TranslateGround : MonoBehaviour
{
    public Transform GroundLeft;
    public bool isGround = false;
    public float moveSpeed;
    public float x;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGround)
        {
            Vector3 movement = Vector2.left * moveSpeed * Time.deltaTime;
            GroundLeft.Translate(movement, Space.Self);
            if (GroundLeft.position.x <= x)
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
