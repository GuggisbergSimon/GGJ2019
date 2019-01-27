using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMessage : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textDisplayed;
	[SerializeField] private Image image;
	private Coroutine currentDialogue;
	private Message currentMessage;

	public void Print(Message message)
	{
		gameObject.SetActive(true);
		if (currentDialogue != null)
		{
			StopCoroutine(currentDialogue);
		}

		currentMessage = message;
		image.sprite = message.sprite;
		if (message.timeBetweenLetters.CompareTo(0) != 0)
		{
			currentDialogue = StartCoroutine(PrintLetterByLetter());
		}
		else
		{
			currentDialogue = StartCoroutine(PrintAll());
		}
	}

	IEnumerator PrintLetterByLetter()
	{
		textDisplayed.text = "";
		for (int i = 0; i < currentMessage.text.Length; i++)
		{
			textDisplayed.text += currentMessage.text[i];
			//todo play a sound here
			yield return new WaitForSeconds(currentMessage.timeBetweenLetters);
		}

		yield return new WaitForSeconds(currentMessage.timeStayEnd);
		gameObject.SetActive(false);
	}

	IEnumerator PrintAll()
	{
		textDisplayed.text = currentMessage.text;
		//todo play a sound here
		yield return new WaitForSeconds(currentMessage.timeStayEnd);
		gameObject.SetActive(false);
	}
}