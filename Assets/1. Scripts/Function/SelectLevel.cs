using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public void Stage(string stage)
    {
        LoadingScene.LoadScene(stage);
    }
}
