using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
/*
  You can modify the fury outside of the code, it has to be between 0 and 100, REALLY IMPORTANT
 */
public class FuryGauge : MonoBehaviour
{
    float furyMax = 100;
    [SerializeField] float furyTime;
    [SerializeField] int fury = 50;
    [SerializeField] Image furyBar;
    [SerializeField] private Sprite[] spriteHead;
    [SerializeField] private Sprite[] spriteHair;

    [SerializeField] private int maxFuryAllowedOpenDoor = 50;
    public int MaxFuryAllowedOpenDoor
    {
        get => maxFuryAllowedOpenDoor;
        set => maxFuryAllowedOpenDoor = value;
    }

    private Image imageHead;
    private Image imageFire;
    private Animator animatorHead;


    private Animator animator;

    public int Fury
    {
        get { return fury; }
        set
        {
            fury = value;
            if (fury / furyMax < 0.07)
            {
                furyBar.fillAmount = 0.07f;
            }
            else
            {
                furyBar.fillAmount = fury / furyMax;
            }
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

        if (fury < maxFuryAllowedOpenDoor)
        {
            animator.SetFloat("Speed", 0.25f);
            imageHead.sprite = spriteHead[0];
            furyBar.sprite = spriteHair[0];
            GameManager.Instance.Camera.ConstantNoise(2, 0.02f);
            animatorHead.SetFloat("Speed", 1 );
        }
        else if (fury > 75)
        {
            animator.SetFloat("Speed", 100f);
            imageHead.sprite = spriteHead[2];
            furyBar.sprite = spriteHair[2];
            GameManager.Instance.Camera.ConstantNoise(5, 5);
            animatorHead.SetFloat("Speed", 50);
        }
        else
        {
            animator.SetFloat("Speed", 1f);
            imageHead.sprite = spriteHead[1];
            furyBar.sprite = spriteHair[1];
            GameManager.Instance.Camera.ConstantNoise(1f, 1f);
            animatorHead.SetFloat("Speed", 5);
        }
        
        GameManager.Instance.UIManager.PostProcessVolume.weight =  (fury / furyMax)* (fury / furyMax);
    }



    IEnumerator DrawFury()
    {
        while (true)
        {
            
            if (fury / furyMax < 0.07)
            {
                furyBar.fillAmount = 0.07f;
            }
            else
            {
                furyBar.fillAmount = fury / furyMax;
            }
            furyBar.color = Color.Lerp(new Color(255f/255, 230f/255, 92f/255), new Color(199f/255, 17f/255, 0f/255), fury/furyMax);
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
