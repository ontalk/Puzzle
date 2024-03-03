using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPortal : MonoBehaviour
{
    public string startPoint;
    private Player thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (thePlayer == null)
            thePlayer = FindObjectOfType<Player>();
        thePlayer.transform.position = transform.position;

    }
}
