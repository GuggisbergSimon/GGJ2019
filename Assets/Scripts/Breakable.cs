using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	[SerializeField] private int resistancePoints = 1;
	private bool _isBroken = false;
	
	public void Damage(int points)
	{
		if (!_isBroken)
		{
			resistancePoints -= points;
			if (resistancePoints <= 0)
			{
				Debug.Log("Bling");
				_isBroken = true;
			}
		}
	}
}
