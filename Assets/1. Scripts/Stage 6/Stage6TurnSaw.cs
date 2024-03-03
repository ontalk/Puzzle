using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6TurnSaw : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.right * moveSpeed *Time.deltaTime;
        transform.Translate(movement, Space.Self); 
    }
}
