using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
	[SerializeField] private Sprite sprite;
	[SerializeField] private string text;
	[SerializeField] private float timeBetweenLetters = 0.01f;
	[SerializeField] private float timeStayEnd = 2.0f;

	private void OnTriggerEnter2D(Collider2D other)
	{
		GameManager.Instance.UIManager.DialoguePopUpSystem.Print(sprite, text, timeBetweenLetters, timeStayEnd);
	}
}