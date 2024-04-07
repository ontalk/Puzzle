using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform Portal1;
    public Transform Portal2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")|| collision.CompareTag("Player2"))
        {
            if (collision.CompareTag("Player") && Portal1 != null)
            {
                collision.transform.position = Portal1.position;
            }
            else if (collision.CompareTag("Player2") && Portal2 != null)
            {
                collision.transform.position = Portal2.position;
            }
        }
    } 
}
