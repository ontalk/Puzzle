using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRanking : MonoBehaviour
{
    private Transform setting;

    private void Awake()
    {
        setting = GameObject.Find("Canvas").transform.Find("SpeedRanking");
    }

    public void Settings()
    {
        GameObject.Find("Canvas").transform.Find("SpeedRanking").gameObject.SetActive(true);
    }
}
