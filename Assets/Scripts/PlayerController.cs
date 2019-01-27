using System.Collections;
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
	[SerializeField] private float furyMaxTime = 1.0f;
	//[SerializeField] private GameObject hitSprite = null;
	//[SerializeField] private float timeSpriteHitAppears = 0.03f;
	private float _horizontalInput;
	private List<GameObject> willBreakNext = new List<GameObject>();
	private Animator _myAnimator;

	public Animator MyAnimator
	{
		get => _myAnimator;
		set => _myAnimator = value;
	}

	private Rigidbody2D _myRigidBody;
	private float _preVerticalInput;
	private float _preHorizontalInput; // la dernière action
	private float _verticalInput;
	private bool _canPunch = true;
	private bool _isLookingRight;

	[FMODUnity.EventRef] [SerializeField] private string eventRefFoot;
	private FMOD.Studio.EventInstance footstep;
	private FMOD.Studio.ParameterInstance randomLoop;
	private FMOD.Studio.ParameterInstance end; // 0 = init, 1 = walk, 1.5 = end, 2 = echo
	private FMOD.Studio.ParameterInstance volumeFoot;
	[SerializeField] private float volumeSoundFoot = 100; // from 0 to 100


	[FMODUnity.EventRef] [SerializeField] private string eventRefHit;
	private FMOD.Studio.EventInstance hit;
	private FMOD.Studio.ParameterInstance volumeHit;
	[SerializeField] private float volumeSoundHit = 100; // from 0 to 100

	private void Awake()
	{
		footstep = FMODUnity.RuntimeManager.CreateInstance(eventRefFoot);
		footstep.getParameter("RandomFoot", out randomLoop);
		footstep.getParameter("end", out end);
		footstep.getParameter("Volume", out volumeFoot);

		hit = FMODUnity.RuntimeManager.CreateInstance(eventRefHit);
		hit.getParameter("Volume", out volumeHit);
	}

	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
		_myAnimator = GetComponent<Animator>();
		FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstep, transform, _myRigidBody);
		FMODUnity.RuntimeManager.AttachInstanceToGameObject(hit, transform, _myRigidBody);
		footstep.start();
		volumeFoot.setValue(volumeSoundFoot * (GameManager.Instance.VolumeMaster / 100) *
							(GameManager.Instance.VolumeSound / 100));
		volumeHit.setValue(volumeSoundHit * (GameManager.Instance.VolumeMaster / 100) *
						   (GameManager.Instance.VolumeSound / 100));
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

		_myAnimator.SetFloat("Speed", _myRigidBody.velocity.y);
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
			hit.start();
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

		if (_horizontalInput < -0.1 || _horizontalInput > 0.1 || _verticalInput < -0.1 || _verticalInput > 0.1)
		{
			end.setValue(1);
		}
		else if (_preHorizontalInput < -0.1 || _preHorizontalInput > 0.1 || _preVerticalInput < -0.1 ||
				 _preVerticalInput > 0.1)
		{
			randomLoop.setValue(Random.Range(0, 2.5f));
			end.setValue(1.5f);
		}

		_preHorizontalInput = _horizontalInput;
		_preVerticalInput = _verticalInput;

		if (GameManager.Instance.UIManager.FuryGauge.Fury >= 100)
		{
			StartCoroutine(FuryMax());
		}
	}

	private IEnumerator FuryMax()
	{
		float timer = 0.0f;
		bool canDie = true;
		while (timer < furyMaxTime)
		{
			if (GameManager.Instance.UIManager.FuryGauge.Fury < 100)
			{
				canDie = false;
			}

			timer += Time.deltaTime;
			yield return null;
		}

		if (canDie)
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

		//hitSprite.SetActive(true);
		//hitSprite.transform.rotation = Quaternion.Euler(Vector3.zero);
		//StartCoroutine(HideGameObjectIn(hitSprite, timeSpriteHitAppears));
		willBreakNext = new List<GameObject>();
		_canPunch = true;
	}

	private IEnumerator HideGameObjectIn(GameObject gameObject, float time)
	{
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}

    private void OnDestroy()
    {
        footstep.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}