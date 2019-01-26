using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	[SerializeField] private int initialResistancePoints = 1;
	[SerializeField] private int furyPoints = 1;
	[SerializeField] private Sprite destroyedSprite = null;
    [SerializeField] private int typeNb;
    private bool _isBroken = false;
	private Collider2D _myCollider;
	private int actualResistancePoints;


    [FMODUnity.EventRef] [SerializeField] private string eventRef;
    private FMOD.Studio.EventInstance breaking;
    private FMOD.Studio.ParameterInstance type; // 1=wood, 2=glasslittle, 3= glassbig, 4= thhhhhing

    private FMOD.Studio.ParameterInstance volume;
    [SerializeField] private float volumeSound = 100;// from 0 to 100

    private void Awake()
    {
        breaking = FMODUnity.RuntimeManager.CreateInstance(eventRef);
        breaking.getParameter("Object", out type);
        breaking.getParameter("Volume", out volume);
    }

    private void Start()
	{
		actualResistancePoints = initialResistancePoints;
		_myCollider = GetComponent<Collider2D>();
	    type.setValue(typeNb);
	    volume.setValue(volumeSound * (GameManager.Instance.VolumeMaster / 100));
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstep, transform, _myRigidBody);
    }

	public void Damage(int points)
	{
		if (!_isBroken)
		{
			actualResistancePoints -= points;
			if (actualResistancePoints <= 0)
			{
			    breaking.start();
				//todo replace the next line by the commented one once we have sprites
				GetComponentInChildren<SpriteRenderer>().color = Color.gray;
				//GetComponentInChildren<SpriteRenderer>().sprite = destroyedSprite;
				_isBroken = true;
			    GameManager.Instance.UIManager.FuryGauge.Fury -= furyPoints;
				Destroy(_myCollider);
				//GameManager.Instance.AStarPath.Scan();
				GameManager.Instance.UIManager.Flash();
				}
			Debug.Log(points);
			GameManager.Instance.Camera.Noise(points, points);
			StartCoroutine(StopShakingCam(initialResistancePoints));
		}
	}

	private IEnumerator StopShakingCam(float time)
	{
		yield return new WaitForSeconds(time);
		GameManager.Instance.Camera.Noise(0,0);
	}
}
