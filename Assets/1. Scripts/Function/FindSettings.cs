using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSettings : MonoBehaviour
{
    public GameObject onOff;
    public GameObject stage;
    public void Settings()
    {
        GameObject.Find("UiCanvas").transform.Find("Settings").gameObject.SetActive(true);
    }

    public void SpeedRanking()
    {
        GameObject.Find("UiCanvas").transform.Find("SpeedRanking").gameObject.SetActive(true);
    }
    public void Timer()
    {
        GameObject timerObject = GameObject.Find("UiCanvas").transform.Find("Timer").gameObject;
        timerObject.SetActive(!timerObject.activeSelf);
    }

    public void TimerOnOff()
    {
        if(stage.activeSelf)
            onOff.SetActive(false);
        else if(!stage.activeSelf)
            onOff.SetActive(true);
    }
}
