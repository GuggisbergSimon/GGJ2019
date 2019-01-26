using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject whiteScreen = null;
	[SerializeField] private GameObject furyObject = null;

	private FuryGauge furyGauge;
	public FuryGauge FuryGauge => furyGauge;

	private void Start()
	{
		furyGauge = furyObject.GetComponent<FuryGauge>();
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
