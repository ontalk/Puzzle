using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySpike : MonoBehaviour
{
    public Rigidbody2D Spike;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Spike.gravityScale = 1;
        }
    }
}
