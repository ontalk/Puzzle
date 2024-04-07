using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage10TranslateSpring : MonoBehaviour
{
    public Transform spring;
    public GameObject spike;
    public float x;
    public float moveSpeed;
    private bool isActive = false;
    private bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            Vector3 movement = Vector2.left * moveSpeed * Time.deltaTime;
            spring.Translate(movement, Space.Self);
            if (spring.position.x <= x)
            {
                isMove = false; 
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMove = true;
            spike.SetActive(true);
        }
    }


}
