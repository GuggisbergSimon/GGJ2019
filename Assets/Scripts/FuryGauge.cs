using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
  You can modify the fury outside of the code, it has to be between 0 and 100, REALLY IMPORTANT
 */
public class FuryGauge : MonoBehaviour
{
    float furyMax = 100;
    [SerializeField] float furyTime;
    int fury;
    [SerializeField] Image furyBar;
    public int Fury
    {
        get { return fury; }
        set { fury = value; }
    }

    private void Start()
    {
        StartCoroutine("DrawFury");
    }

    private void Update()
    {
        if (fury < 0)
        {
            fury = 0;
        }
    }

    IEnumerator DrawFury()
    {
        for (fury = 0; fury < furyMax; fury++)
        {
            furyBar.fillAmount = fury / furyMax;
            yield return new WaitForSeconds((1*furyTime)/furyMax);
        }
        furyBar.fillAmount = 1;
        //Load Scene Game Over
    }
}
