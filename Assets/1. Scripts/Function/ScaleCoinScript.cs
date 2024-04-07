using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCoinScript : MonoBehaviour
{
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!Player.Instance.isScale)
            {
                collision.GetComponent<Player>().CharacterScale();
            }
            else if(Player.Instance.isScale)
            {
                collision.GetComponent<Player>().OriginalCharacterScale();
            }
            Destroy(gameObject);
        }
    }
}
