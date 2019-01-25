using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    IEnumerator DrawFury()
    {
        for (fury = 0; fury < furyMax; fury++)
        {
            furyBar.fillAmount = fury / furyMax;
            yield return new WaitForSeconds((1*furyTime)/furyMax);
        }
        furyBar.fillAmount = 1;
    }
}
