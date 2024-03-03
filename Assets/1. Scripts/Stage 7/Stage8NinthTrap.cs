using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8NinthTrap : MonoBehaviour, IMoveGameobject
{
    public Transform spike;
    public float speed;
    private bool isTrap= false;
    private bool isActive= false;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive&&isTrap) 
        {
            
            Move(Vector2.right, speed);
            if (spike.position.x >= x)
                isActive = true;
        }
        
    }
    public void Move(Vector2 d, float s)
    {
        spike.Translate(d * s * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTrap = true;
        }
    }
}
