using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage8FirstTrap : MonoBehaviour, IMoveGameobject
{
    public Transform Ground;
    private bool isGround = false;
    private bool isActive = false;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGround && !isActive)
        {
            Move(Vector2.down, 10f);
            StartCoroutine(DownGround());
        }
    }

    public void Move(Vector2 t, float s) 
    {
        Ground.Translate(t * s * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            isGround = true;
        }
    }

    IEnumerator DownGround()
    {
        
        while(Ground.position.y >=-1.32f)
        {
            yield return null;
            
        }
        Move(Vector2.left, 10f);

        while(Ground.position.x >=-1.2f)
        {
            yield return null;
        }
        Move(Vector2.up, 10f);

        while (Ground.position.y <= 0f)
        {
            yield return null;
            isActive = true;
        }

    }

}
