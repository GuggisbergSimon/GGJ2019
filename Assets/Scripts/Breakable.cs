﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	[SerializeField] private int initialResistancePoints = 1;
	[SerializeField] private int furyPoints = 1;
	[SerializeField] private Sprite destroyedSprite = null;
	[SerializeField] private Color destroyedColor = Color.gray;
    [SerializeField] private int typeNb;
	private bool _isBroken = false;
	private Collider2D _myCollider;
	private int _actualResistancePoints;
	private SpriteRenderer _mySpriteRenderer;


	[FMODUnity.EventRef] [SerializeField] private string eventRef;
	private FMOD.Studio.EventInstance breaking;
	private FMOD.Studio.ParameterInstance type; // 1=wood, 2=glasslittle, 3= glassbig, 4= thhhhhing

	private FMOD.Studio.ParameterInstance volume;
	[SerializeField] private float volumeSound = 100; // from 0 to 100

	private void Awake()
	{
		breaking = FMODUnity.RuntimeManager.CreateInstance(eventRef);
		breaking.getParameter("Object", out type);
		breaking.getParameter("Volume", out volume);
	}

	private void Start()
	{
		_mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
		_actualResistancePoints = initialResistancePoints;
		_myCollider = GetComponent<Collider2D>();
		type.setValue(typeNb);
		volume.setValue(volumeSound * (GameManager.Instance.VolumeMaster / 100) * (GameManager.Instance.VolumeSound / 100));
		//FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstep, transform, _myRigidBody);
	}

	public void Damage(int points)
	{
		if (!_isBroken)
		{
			_actualResistancePoints -= points;
			if (_actualResistancePoints <= 0)
			{
				breaking.start();
				//todo replace the next line by the commented one once we have sprites

			    if (destroyedSprite)
			    {
			        GetComponentInChildren<SpriteRenderer>().sprite = destroyedSprite;
                }
			    GetComponentInChildren<SpriteRenderer>().color = destroyedColor;

                _isBroken = true;
				_mySpriteRenderer.sortingOrder -= 1000;
				
			    if (GameManager.Instance.UIManager.FuryGauge.Fury < 25)
			    {
			        GameManager.Instance.UIManager.FuryGauge.Fury -= (int)(furyPoints * 0.5f);
                }
			    else if (GameManager.Instance.UIManager.FuryGauge.Fury > 75)
			    {
			        GameManager.Instance.UIManager.FuryGauge.Fury -= (int)(furyPoints * 2f);
                }
			    else
			    {
			        GameManager.Instance.UIManager.FuryGauge.Fury -= (int)(furyPoints * 1f);
                }
				Destroy(_myCollider);
				//GameManager.Instance.AStarPath.Scan();
				GameManager.Instance.UIManager.Flash();
			    if (GetComponent<Computer>())
			    {
			        GameManager.Instance.GameOver();
			    }
			}

			GameManager.Instance.Camera.Noise(points, points);
			GameManager.Instance.Camera.StopShakingCam(initialResistancePoints/2.0f);
		}
	}
}