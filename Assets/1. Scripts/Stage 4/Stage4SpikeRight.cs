using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4SpikeRight : MonoBehaviour
{
    public Transform SpikeRight;
    public Transform Gournd;
    public bool isSpike = false;
    public bool isGround = false;
    public float moveSpeed;
    public float x;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isSpike)
        {
            Vector3 movement = Vector2.right * moveSpeed * Time.deltaTime;
            SpikeRight.Translate(movement, Space.Self);
            if (SpikeRight.position.x >= x)
            {
                isSpike = false;
            }
        }

        if (isGround)
        {
            Vector3 movement = Vector2.right * moveSpeed * Time.deltaTime;
            SpikeRight.Translate(movement, Space.Self);
            if (SpikeRight.position.x >= 37.7f)
            {
                isGround = false;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isSpike = true;
        }
        if(collision.gameObject.tag == "Player" && !isSpike)
        {
            isGround = true;
        }
    }
}
