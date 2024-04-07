using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage10Active : MonoBehaviour
{
    public GameObject Spring;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Spring.gameObject.SetActive(true);
    }
}
