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
    public AudioClip SpikeClip;//�����
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

            //Trap.transform.Translate(new Vector3(10f, 0f, 0f)); //<- Ʈ���� x�� 10��ŭ �̵��ϱ�
            Trap.transform.position = new Vector3(2, -1, 0); // ������ǥ
            SoundManager.instance.SfXPlay("Spike", SpikeClip);
            gameObject.SetActive(false);
        }
    }

}
