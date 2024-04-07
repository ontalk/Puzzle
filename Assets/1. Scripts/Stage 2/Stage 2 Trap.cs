using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage2Trap : MonoBehaviour
{
    private GameObject Trap;
    private Vector3 originalTransform;
    private Player player;
    public AudioClip SpikeClip;//오디오
    // Start is called before the first frame update
    void Awake()
    {
        originalTransform = transform.position;
        Trap = GameObject.FindGameObjectWithTag("Trap");
        player = GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
            
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            //Trap.transform.Translate(new Vector3(10f, 0f, 0f)); //<- 트랩을 x축 10만큼 이동하기
            Trap.transform.position = new Vector3(2, -1, 0); // 절대좌표
            SoundManager.instance.SfXPlay("Spike", SpikeClip);
            gameObject.SetActive(false);
        }
    }

}
