﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	[SerializeField] private int resistancePoints = 1;
	[SerializeField] private int furyPoints = 1;
	[SerializeField] private Sprite destroyedSprite = null;
	private bool _isBroken = false;
	
	public void Damage(int points)
	{
		if (!_isBroken)
		{
			//todo add furypoints to fury and update furygauge
			resistancePoints -= points;
			if (resistancePoints <= 0)
			{
				//todo replace the next line by the commented one
				GetComponentInChildren<SpriteRenderer>().color = Color.gray;
				//GetComponentInChildren<SpriteRenderer>().sprite = destroyedSprite;
				_isBroken = true;
			}
		}
	}
}
