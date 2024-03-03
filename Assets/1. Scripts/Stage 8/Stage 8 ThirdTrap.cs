using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8ThirdTrap : MonoBehaviour, IMoveGameobject
{
    public Transform Ground;
    private bool isGround = false;
    private bool isActive = false;

    void Update()
    {
        if (isGround && !isActive)
        {
            Move(Vector2.up, 50f);
            if (Ground.position.y >= 13f)
                isActive = true;
        }
    }

    public void Move(Vector2 t, float s)
    {
        Ground.Translate(t * s * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isGround = true;
        }
    }

}
