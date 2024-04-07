using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WhileUpDown : MonoBehaviour
{
    public float downY;
    public float upY;
    public bool downPortal = false;
    public float speed;
    private Transform Portal;
    private void Start()
    {
        Portal = GetComponent<Transform>();   
    }
    void Update()
    {
        if (downPortal)
        {
            Portal.Translate(Vector2.down * speed *Time.deltaTime, Space.Self);
            if (Portal.position.y <= downY)
                downPortal = false;
        }
        else if(!downPortal)
        {
            Portal.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
            if (Portal.position.y >= upY)
                downPortal = true;
        }
    }
}
