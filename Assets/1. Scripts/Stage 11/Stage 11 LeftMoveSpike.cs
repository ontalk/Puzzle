using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage11LeftMoveSpike : MonoBehaviour
{
    private bool isSaw = false;
    private bool isActive = false;
    public GameObject spring;
    public float x;
    public Transform Saw;


    // Update is called once per frame
    void Update()
    {
        if (isSaw && !isActive)
        {
            Move(Vector2.up, 30);
            if (Saw.position.x <= x)
                isActive = true;
        }
    }

    public void Move(Vector2 direction, float speed)
    {
        Saw.Translate(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isSaw = true;
            if(spring != null)
                spring.SetActive(true);

        }
    }
}
