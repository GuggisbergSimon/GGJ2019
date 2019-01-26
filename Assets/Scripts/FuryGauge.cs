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

    private Image imageHead;
    private Animator animatorHead;


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
        animatorHead = GameManager.Instance.UIManager.Head.GetComponent<Animator>();
        imageHead = GameManager.Instance.UIManager.Head.GetComponent<Image>();
    }

    private void Update()
    {
        if (fury < 0)
        {
            fury = 0;
        }

        if (fury < 35)
        {
            animator.SetFloat("Speed", 0.25f);
            GameManager.Instance.Camera.ConstantNoise(2, 0.02f);
            imageHead.color = Color.green;
            animatorHead.SetFloat("Speed", 1 );
        }
        else if (fury > 75)
        {
            animator.SetFloat("Speed", 100f);
            GameManager.Instance.Camera.ConstantNoise(5, 5);
            imageHead.color = Color.red;
            animatorHead.SetFloat("Speed", 50);


        }
        else
        {
            animator.SetFloat("Speed", 1f);
            GameManager.Instance.Camera.ConstantNoise(1f, 1f);
            imageHead.color = Color.yellow;
            animatorHead.SetFloat("Speed", 5);


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
