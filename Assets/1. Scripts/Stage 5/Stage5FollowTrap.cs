using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5FollowTrap : MonoBehaviour
{
    public Transform SpikeRight;
    public bool isSpike = false;
    public float moveSpeed;
    private void Update()
    {
        if (isSpike)
        {
            Vector3 movement = Vector2.right * moveSpeed * Time.deltaTime;
            SpikeRight.Translate(movement, Space.Self);
            if (SpikeRight.position.x >= 21.3f)
            {
                isSpike = false;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isSpike = true;
        }
    }
}
