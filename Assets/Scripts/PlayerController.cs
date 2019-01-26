﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speed = 2.0f;
	[SerializeField] private float interactRadius = 0.5f;
	[SerializeField] private int damage = 1;
	[SerializeField] private GameObject aim = null;
	private float _horizontalInput;
	private List<GameObject> willBreakNext = new List<GameObject>();
    public Animator MyAnimator
    private Animator _myAnimator;
    private Rigidbody2D _myRigidBody;
    private float _preVerticalInput;
    private float _preHorizontalInput;// la dernière action
    private float _verticalInput;
	private bool _canPunch = true;
	private bool _isLookingRight;

	[FMODUnity.EventRef] [SerializeField] private string eventRef;
	private FMOD.Studio.EventInstance footstep;
	private FMOD.Studio.ParameterInstance randomLoop;
	private FMOD.Studio.ParameterInstance end; // 0 = init, 1 = walk, 1.5 = end, 2 = echo
	private FMOD.Studio.ParameterInstance volume;
	[SerializeField] private float volumeSound = 100; // from 0 to 100

	private void Awake()
	{
		footstep = FMODUnity.RuntimeManager.CreateInstance(eventRef);
		footstep.getParameter("RandomFoot", out randomLoop);
		footstep.getParameter("end", out end);
		footstep.getParameter("Volume", out volume);
	}

	private void Start()
	{
		_myAnimator = GetComponentInChildren<Animator>();
		_myRigidBody = GetComponent<Rigidbody2D>();
		_myAnimator = GetComponent<Animator>();
	    FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstep, transform, _myRigidBody);
	    footstep.start();
	    volume.setValue(volumeSound * (GameManager.Instance.VolumeMaster / 100));
	}

	private void FixedUpdate()
	{
		if (_canPunch)
		{
			_myRigidBody.velocity = speed * (Vector2.right * _horizontalInput + Vector2.up * _verticalInput);
			if (_myRigidBody.velocity != Vector2.zero)
			{
				aim.transform.up = _myRigidBody.velocity;
			}
		}

		_myAnimator.SetFloat("Speed", _myRigidBody.velocity.magnitude);
		if ((_isLookingRight && _myRigidBody.velocity.x < 0) || (!_isLookingRight) && _myRigidBody.velocity.x > 0)
		{
			_isLookingRight = !_isLookingRight;
			transform.rotation = Quaternion.Euler(0, (transform.rotation.eulerAngles.y + 180) % 360, 0);
		}
	}

	private void Update()
	{
		_horizontalInput = Input.GetAxis("Horizontal");
		_verticalInput = Input.GetAxis("Vertical");
		if (Input.GetButtonDown("Jump") && _canPunch)
		{
			_canPunch = false;
			_myAnimator.SetTrigger("Attack");
			//todo find a more elegant way to get the child of aim
			Collider2D[] hits =
				Physics2D.OverlapCircleAll(aim.transform.GetChild(0).transform.position, interactRadius);
			foreach (var hit in hits)
			{
				if (hit && hit.CompareTag("Breakable"))
				{
					willBreakNext.Add(hit.gameObject);
				}
			}
		}

	    _preHorizontalInput = _horizontalInput;
	    _preVerticalInput = _verticalInput;

	    if (GameManager.Instance.UIManager.FuryGauge.Fury >= 100)
	    {
            _myAnimator.SetTrigger("Death");
	    }
	}

    public void GameOver()
    {
        GameManager.Instance.LoadScene("GameOver");
    }

	public void Break()
	{
		foreach (var hit in willBreakNext)
		{
			hit.GetComponent<Breakable>().Damage(damage);
		}

		willBreakNext = new List<GameObject>();
		_canPunch = true;
	}
}