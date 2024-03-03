using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage5SawPosition : MonoBehaviour
{
    public Transform Saw;
    public float x;
    public float y;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            gameObject.SetActive(false);
            //Saw.transform.position = new Vector2(10.33f,-5.7f); // Àý´ëÁÂÇ¥
            Saw.transform.position = new Vector2(Saw.transform.position.x + 2f, Saw.transform.position.y);
        }
    }
}
