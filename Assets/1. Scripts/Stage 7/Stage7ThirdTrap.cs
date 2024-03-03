using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage7ThirdTrap : MonoBehaviour
{
    public List<Transform> Spike;
    private bool isSpike = false;
    private bool isActive = true;
    public float y;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpike && isActive)
        {
            StartCoroutine(SpikeUp());
        }
    }

    IEnumerator SpikeUp()
    {

        foreach(Transform spike in Spike)
        {
            yield return new WaitForSeconds(0.1f);
            spike.Translate(new Vector2(0f, y));
            isActive = false;
        }
        StartCoroutine(SpikeDown());
    }

    IEnumerator SpikeDown()
    {
        yield return new WaitForSeconds(2f);

        foreach (Transform spike in Spike)
        {
            yield return new WaitForSeconds(0.1f);
            spike.Translate(new Vector2(0f, -1f));
        }
    }
    

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            isSpike = true;
        }
    }
}
