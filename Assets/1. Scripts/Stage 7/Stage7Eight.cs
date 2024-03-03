using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage7Eight : MonoBehaviour, IMoveGameobject
{
    public Transform StagePortal;
    public Transform spike;
    public float stageX;
    public float spikeX;
    private bool isMove = false;
    private bool isSpikeActive = false;
    private bool isStageActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove &&!isStageActive)
        {
            Move(Vector2.right, 10f);
            if (StagePortal.position.x >= stageX)
                isStageActive = true;
        }

    }

    public void Move(Vector2 direction,float speed)
    {
        StagePortal.Translate(direction*speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            spike.gameObject.SetActive(true);
            isMove = true;
        }
    }
}
