using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player2");
        player.transform.position = transform.position;
    }

    void Update()
    {



    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.isDead)
            {
                StartCoroutine(SpawnPlayer(player));
            }
        }
    }

    private IEnumerator SpawnPlayer(Player player)
    {
        yield return new WaitForSeconds(2f);

        player.isDead = false;
        player.transform.position = transform.position;
        player.gameObject.SetActive(true);
    }
}
