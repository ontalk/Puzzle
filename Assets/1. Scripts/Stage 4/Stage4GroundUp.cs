using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4GroundUp : MonoBehaviour
{
    public Transform GroundUp;
    public List<GameObject> Spike;
    public bool isGround = false;
    public bool isSpike = true;
    public float moveSpeed;
    void Start()
    {
        if (GroundUp == null)
            Debug.Log("없는게 맞음");
        if (Spike == null)
            Debug.Log("없는게 맞음");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGround)
        {
            Vector3 movement = Vector2.up * moveSpeed * Time.deltaTime;
            GroundUp.Translate(movement, Space.Self);
            if (GroundUp.position.y >= 8f)
            {
                isGround = false;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isGround)
        {
            isGround = true;

        }
        if (collision.gameObject.tag == "Player" )
        {
            foreach (GameObject spike in Spike)
            {
                spike.SetActive(true);
            }
        }

    }

}
