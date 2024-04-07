using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stage11Player2Die : MonoBehaviour
{
    public string stage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
            SceneManager.LoadScene(stage);
    }
}
