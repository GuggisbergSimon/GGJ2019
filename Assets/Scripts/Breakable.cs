using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	[SerializeField] private int resistancePoints = 1;
	[SerializeField] private int furyPoints = 1;
	[SerializeField] private Sprite destroyedSprite = null;
    [SerializeField] private int typeNb;
    private bool _isBroken = false;
	private Collider2D _myCollider;


    [FMODUnity.EventRef] [SerializeField] private string eventRef;
    private FMOD.Studio.EventInstance breaking;
    private FMOD.Studio.ParameterInstance type; // 1=wood, 2=glasslittle, 3= glassbig, 4= thhhhhing

    private void Awake()
    {
        breaking = FMODUnity.RuntimeManager.CreateInstance(eventRef);
        breaking.getParameter("Object", out type);
    }

    private void Start()
	{
		_myCollider = GetComponent<Collider2D>();
	    type.setValue(typeNb);
	    //FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstep, transform, _myRigidBody);
	}

	public void Damage(int points)
	{
		if (!_isBroken)
		{
			//todo add furypoints to fury and update furygauge
			resistancePoints -= points;
			if (resistancePoints <= 0)
			{
				//todo play breaking sound
			    breaking.start();
				//todo replace the next line by the commented one
				GetComponentInChildren<SpriteRenderer>().color = Color.gray;
				//GetComponentInChildren<SpriteRenderer>().sprite = destroyedSprite;
				_isBroken = true;
			    GameManager.Instance.FuryGauge.Fury -= furyPoints;
				Destroy(_myCollider);
			}
		}
	}
}
