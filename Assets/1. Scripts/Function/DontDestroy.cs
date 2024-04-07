using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    GameObject[] UiCanvas;

    void Awake()
    {
        UiCanvas = GameObject.FindGameObjectsWithTag("UiCanvas");
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

        if (UiCanvas.Length > 1)
        {
            Destroy(UiCanvas[1]);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
