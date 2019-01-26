﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject whiteScreen = null;
	[SerializeField] private GameObject furyObject = null;
	[SerializeField] private GameObject postProcessObject = null;

    private FuryGauge furyGauge;
	public FuryGauge FuryGauge => furyGauge;

    private PostProcessVolume postProcessVolume;
    public PostProcessVolume PostProcessVolume => postProcessVolume;

	private void Start()
	{
		furyGauge = furyObject.GetComponent<FuryGauge>();
	    postProcessVolume = postProcessObject.GetComponent<PostProcessVolume>();
	}

	private IEnumerator Flashing()
	{
		whiteScreen.SetActive(true);
		yield return null;
		whiteScreen.SetActive(false);
	}

	public void Flash()
	{
		StartCoroutine(Flashing());
	}
}
