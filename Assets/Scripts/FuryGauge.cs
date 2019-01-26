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
    int fury = 50;
    [SerializeField] Image furyBar;

    private Animator animator;

    public int Fury
    {
        get { return fury; }
        set
        {
            fury = value;
            furyBar.fillAmount = fury / furyMax;
        }
    }

    private void Start()
    {
        StartCoroutine("DrawFury");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (fury < 0)
        {
            fury = 0;
        }

        if (fury < 25)
        {
            animator.SetFloat("Speed", 0.25f);

        }
        else if (fury > 75)
        {
            animator.SetFloat("Speed", 10f);
        }
        else
        {
            animator.SetFloat("Speed", 1f);
        }

        GameManager.Instance.UIManager.PostProcessVolume.weight =  (fury / furyMax)* (fury / furyMax);
    }



    IEnumerator DrawFury()
    {
        while (true)
        {
            furyBar.fillAmount = fury / furyMax;
            if (fury < 100)
            {
                fury++;
            }
            yield return new WaitForSeconds((1 * furyTime) / furyMax);
        }
        furyBar.fillAmount = 1;
        //Load Scene Game Over
    }
}
