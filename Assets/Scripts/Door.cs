using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private GameObject nextRoom;
	[SerializeField] private GameObject actualRoom;

	private BoxCollider2D boxCollider2D;


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
		boxCollider2D = GetComponent<BoxCollider2D>();
		volume.setValue(volumeSound * (GameManager.Instance.VolumeMaster / 100));
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.Instance.UIManager.FuryGauge.Fury < 50)
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") || GameManager.Instance.UIManager.FuryGauge.Fury < 50)
		{
			doorSound.start();
			GameManager.Instance.Camera.MoveRoomFunction(nextRoom.transform.position);
			int numberCaseToGoBetweenRooms = 2;
			other.transform.position += (nextRoom.transform.position - actualRoom.transform.position).normalized *
										numberCaseToGoBetweenRooms;
			nextRoom.SetActive(true);
			actualRoom.SetActive(false);
			GameManager.Instance.UIManager.FuryGauge.Fury = 100;
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Rage");
		doorSound.start();
	}
}