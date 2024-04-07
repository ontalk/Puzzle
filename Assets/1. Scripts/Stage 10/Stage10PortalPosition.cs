using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Stage10PortalPosition : MonoBehaviour
{
    public Transform Portal;
    public GameObject spike;
    public float x;
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
        if (collision.gameObject.tag == "Player")
        {
            Portal.position = new Vector2(x,y);
            spike.SetActive(true);
        }
    }
}
