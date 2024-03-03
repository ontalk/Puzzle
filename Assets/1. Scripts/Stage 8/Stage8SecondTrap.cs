using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8SecondTrap : MonoBehaviour ,IMoveGameobject
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
        if (isGround && !isActive)
        {
            Move(Vector2.down, 15f);
            if(Ground.position.y <=-10f)
                isActive = true;
        }
    }

    public void Move(Vector2 t, float s)
    {
        Ground.Translate(t * s * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isGround = true;
        }
    }

}
