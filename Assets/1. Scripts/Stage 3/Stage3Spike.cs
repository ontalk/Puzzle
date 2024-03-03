using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage3Spike : MonoBehaviour
{
    GameObject Trap;
    Vector2 originalPosition;
    public Transform colliderTransform;
    // Start is called before the first frame update
    void Start()
    {
        Trap = GameObject.FindGameObjectWithTag("ForTrap");
        originalPosition = colliderTransform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colliderTransform.transform.position = originalPosition;
            Trap.SetActive(true);
        }
    }
}
