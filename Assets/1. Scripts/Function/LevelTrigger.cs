using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class LevelTrigger : MonoBehaviour
{
    public int index;
    public string Name;
    public int Achieved;
    public string stage;
    // Start is called before the first frame update
    void Start()
    {
        Achieved = PlayerPrefs.GetInt(Name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Achieved == 0)
            {
                index++;
                Achieved++;
                PlayerPrefs.SetInt("highestLevel", index);
                PlayerPrefs.SetInt(Name, Achieved);
                PlayerPrefs.Save();
                LoadingScene.LoadScene(stage);
            }


            if (Achieved == 1)
            {
                Debug.Log("You played this level before No reward");
                LoadingScene.LoadScene(stage);
            }
        }
    }
}
