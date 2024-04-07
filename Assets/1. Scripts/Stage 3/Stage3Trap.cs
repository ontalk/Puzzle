using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Trap : MonoBehaviour
{
    //private GameObject Ground;
    public Transform colliderTransform;
    public AudioClip TranformGroundClip;
    // Start is called before the first frame update
    void Start()
    {
        //Ground = GameObject.FindGameObjectWithTag("Ground");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            colliderTransform.transform.Translate(new Vector2(1.8f, 0));
            SoundManager.instance.SfXPlay("Ground", TranformGroundClip);
            gameObject.SetActive(false);
        }
    }
}
