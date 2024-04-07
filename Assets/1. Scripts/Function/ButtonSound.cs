using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
   
    public void Buttonsound(AudioClip buttonClip)
    {
        SoundManager.instance.SfXPlay("button", buttonClip);
    }
}
