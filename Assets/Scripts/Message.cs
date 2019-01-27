using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new Message",fileName = "messageSO")]
public class Message : ScriptableObject
{
	public Sprite sprite;
	public string text;
	public float timeBetweenLetters = 0.01f;
	public float timeStayEnd = 3.0f;
}
