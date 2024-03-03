using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6GroundDown : MonoBehaviour
{
    public Transform GroundDown;
    public float minHeight;
    private Vector2 originalDownPosition;
    public float x;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
        originalDownPosition = GroundDown.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GroundDownRepeat();
    }

    void GroundDownRepeat()
    {
        float moveSpeed = Random.Range(3f, 5f);
        Vector3 movement = Vector2.down * moveSpeed * Time.deltaTime;
        GroundDown.Translate(movement, Space.Self);
        if (GroundDown.position.y <= minHeight)
        {
            GroundDown.transform.position = new Vector2(x,y);
        } 
    }
}
