using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //DontDestroyOnLoad(this);
    }
}
