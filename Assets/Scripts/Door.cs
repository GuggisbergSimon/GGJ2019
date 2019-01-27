using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private GameObject nextRoom;
	[SerializeField] private GameObject actualRoom;
	[SerializeField] private int furyAddedAfterOpening = 50;
	[SerializeField] private Message stuckMessage;

	private BoxCollider2D boxCollider2D;
	private SpriteRenderer mySpriteRenderer;
	private bool isOpen = false;
	
	[FMODUnity.EventRef] [SerializeField] private string eventRef;
	private FMOD.Studio.EventInstance doorSound;
	private FMOD.Studio.ParameterInstance doorState;

	private FMOD.Studio.ParameterInstance volume;
	[SerializeField] private float volumeSound = 100; // from 0 to 100

	private void Awake()
	{
		doorSound = FMODUnity.RuntimeManager.CreateInstance(eventRef);
		doorSound.getParameter("DoorState", out doorState);
		doorSound.getParameter("Volume", out volume);
	}


	void Start()
	{
		mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		volume.setValue(volumeSound * (GameManager.Instance.VolumeMaster / 100) * (GameManager.Instance.VolumeSound / 100));
	}

	void Update()
	{
	    if (!isOpen)
	    {
	        if (GameManager.Instance.UIManager.FuryGauge.Fury <
	            GameManager.Instance.UIManager.FuryGauge.MaxFuryAllowedOpenDoor)
	        {
	            boxCollider2D.isTrigger = true;
	            doorState.setValue(1);
	        }
	        else
	        {
	            boxCollider2D.isTrigger = false;
	            doorState.setValue(0);
            }
	    }
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!isOpen && other.CompareTag("Player") && GameManager.Instance.UIManager.FuryGauge.Fury < GameManager.Instance.UIManager.FuryGauge.MaxFuryAllowedOpenDoor)
		{
			doorSound.start();
			nextRoom.SetActive(true);
			mySpriteRenderer.gameObject.SetActive(false);
			isOpen = true;
			GameManager.Instance.UIManager.FuryGauge.Fury += furyAddedAfterOpening;
		}
	}

    private void OnCollisionEnter2D(Collision2D other)
	{
		GameManager.Instance.UIManager.DialoguePopUpSystem.Print(stuckMessage);
        doorSound.start();
    }
    

}