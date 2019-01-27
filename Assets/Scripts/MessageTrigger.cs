using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
	[SerializeField] private Message message;
	private void OnTriggerEnter2D(Collider2D other)
	{
		GameManager.Instance.UIManager.DialoguePopUpSystem.Print(message);
	}
}