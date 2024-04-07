using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7FourthTrap : MonoBehaviour, IMoveGameobject
{
    private bool isSaw = false;
    private bool isActive = false;
    public Transform Saw;
    public float speed = 10;

    // Update is called once per frame
    void Update()
    {
        if (isSaw && !isActive)
        {
            Move(Vector2.down, speed);
            if(Saw.position.x <= -18f)
                isActive = true;
        }
    }

    public void Move(Vector2 direction, float speed)
    {
        Saw.Translate(direction * speed* Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isSaw = true;
    }
}
