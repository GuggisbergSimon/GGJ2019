using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMessage : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textDisplayed;
	[SerializeField] private Image image;
	private string currentText;
	private float currentTimeStayAtEnd;
	private Coroutine currentDialogue;

	public void Print(Sprite sprite, string text, float timeBetweenLetters, float timeStayEnd)
	{
		gameObject.SetActive(true);
		if (currentDialogue != null)
		{
			StopCoroutine(currentDialogue);
		}

		currentText = text;
		image.sprite = sprite;
		currentTimeStayAtEnd = timeStayEnd;
		if (timeBetweenLetters.CompareTo(0) != 0)
		{
			currentDialogue = StartCoroutine(PrintLetterByLetter(timeBetweenLetters));
		}
		else
		{
			currentDialogue = StartCoroutine(PrintAll());
		}
	}

	IEnumerator PrintLetterByLetter(float timeBetweenLetters)
	{
		textDisplayed.text = "";
		for (int i = 0; i < currentText.Length; i++)
		{
			textDisplayed.text += currentText[i];
			//todo play a sound here
			yield return new WaitForSeconds(timeBetweenLetters);
		}

		yield return new WaitForSeconds(currentTimeStayAtEnd);
		gameObject.SetActive(false);
	}

	IEnumerator PrintAll()
	{
		textDisplayed.text = currentText;
		//todo play a sound here
		yield return new WaitForSeconds(currentTimeStayAtEnd);
		gameObject.SetActive(false);
	}
}