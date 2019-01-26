using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
	[SerializeField] private float timeBetweenChangingColor;
	private SpriteRenderer _mySpriteRenderer;

	private void Start()
	{
		_mySpriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(ChangeHSV());
	}

	private IEnumerator ChangeHSV()
	{
		float h = 0.0f;
		while (true)
		{
			_mySpriteRenderer.color = Color.HSVToRGB(h % 1, 1, 0.5f);
			h += 1 / 360.0f;
			yield return new WaitForSeconds(timeBetweenChangingColor);
		}
	}
}