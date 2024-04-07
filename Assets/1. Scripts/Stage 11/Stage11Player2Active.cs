using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage11Player2Active : MonoBehaviour
{
    public GameObject Spring;
    public GameObject spike;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            Spring.gameObject.SetActive(true);
            spike.gameObject.SetActive(true);
        }
    }
}
