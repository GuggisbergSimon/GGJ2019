﻿using System.Collections;
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
    [SerializeField] private float volumeSound = 100;// from 0 to 100

    private void Awake()
    {
        doorSound = FMODUnity.RuntimeManager.CreateInstance(eventRef);
        doorSound.getParameter("DoorState", out doorState);
        doorSound.getParameter("Volume", out volume);
    }


    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        volume.setValue(volumeSound*(GameManager.Instance.VolumeMaster/100));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.UIManager.FuryGauge.Fury < 35)
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
        if (other.CompareTag("Player") || GameManager.Instance.UIManager.FuryGauge.Fury < 25)
        {
            doorSound.start();
            GameManager.Instance.Camera.MoveRoomFunction(nextRoom.transform.position);
            GameManager.Instance.Player.transform.position += Vector3.up*2*(Mathf.Sign(nextRoom.transform.position.y-actualRoom.transform.position.y));
            nextRoom.SetActive(true);
            actualRoom.SetActive(false);
            GameManager.Instance.UIManager.FuryGauge.Fury = 50;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Rage");
        doorSound.start();
    }
}
