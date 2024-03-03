using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Spike : MonoBehaviour
{
    private Vector2 originalPosition;

    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
             transform.position = originalPosition;
        }
    }

}
