using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUpDown : MonoBehaviour
{
    public Transform GroundUp;
    public float moveSpeed;
    public float maxHeight;
    public float x;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GroundUpRepeat();
    }

    void GroundUpRepeat()
    {
        Vector3 movement = Vector2.up * moveSpeed * Time.deltaTime;
        GroundUp.Translate(movement, Space.Self);
        if (GroundUp.position.y >= maxHeight)
        {
            GroundUp.transform.position = new Vector2(x, y) ;
        }
    }
    
}
