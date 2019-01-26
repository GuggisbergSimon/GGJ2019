using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject whiteScreen = null;
	[SerializeField] private GameObject furyObject = null;
	[SerializeField] private float flashDuration = 0.01f;
	private int numberFlash;
	private FuryGauge furyGauge;
	public FuryGauge FuryGauge => furyGauge;

	private void Start()
	{
		furyGauge = furyObject.GetComponent<FuryGauge>();
	}

	private IEnumerator Flashing()
	{
		whiteScreen.SetActive(true);
		yield return new WaitForSeconds(flashDuration);
		numberFlash--;
		if (numberFlash <= 0)
		{
			whiteScreen.SetActive(false);
		}
	}

	public void Flash()
	{
		numberFlash++;
		StartCoroutine(Flashing());
	}
}
