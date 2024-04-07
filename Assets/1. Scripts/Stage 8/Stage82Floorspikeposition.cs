using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage82Floorspikeposition : MonoBehaviour
{
    public Transform spike;
    public float y;
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
        if(collision.gameObject.tag=="Player2")
        {
            spike.transform.position = new Vector2(spike.transform.position.x, spike.transform.position.y + y);
        }
    }
}
