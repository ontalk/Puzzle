using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8PortalPosition : MonoBehaviour
{
    public Transform Portal;
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
        if (collision.gameObject.tag == "Player2"|| collision.gameObject.tag == "Player")
        {
            Portal.transform.position = new Vector2(Portal.transform.position.x + x , Portal.transform.position.y + y);
            gameObject.SetActive(false);
        }
    }
}
