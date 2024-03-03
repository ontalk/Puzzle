using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StagePortal : MonoBehaviour
{
    public string transferMapName;
    public int nextSceneLoad;
    private Player thePlayer;

    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        if (thePlayer == null)
            thePlayer = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LoadingScene.LoadScene(transferMapName);
            if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }
}
