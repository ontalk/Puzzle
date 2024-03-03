using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7SixthTrap : MonoBehaviour, IMoveGameobject
{
    private bool isMove = false;
    private bool isActive = false;
    public Transform spike;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove && !isActive)
        {
            Move(Vector2.right, 10);
            if(spike.position.x>= x)
                isActive = true;
        }
    }

    public void Move(Vector2 direction, float speed)
    {
        spike.Translate(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isMove = true;
    }
}
