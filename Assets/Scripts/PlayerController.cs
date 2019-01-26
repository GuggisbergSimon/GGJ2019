using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speed = 2.0f;
	[SerializeField] private float interactRadius = 0.5f;
	[SerializeField] private int damage = 1;
	[SerializeField] private GameObject aim = null;
	private float _horizontalInput;
    private float _verticalInput;
    private float _preHorizontalInput;// la dernière action
    private float _preVerticalInput;
    private Rigidbody2D _myRigidBody;


    [FMODUnity.EventRef] [SerializeField] private string eventRef;
    private FMOD.Studio.EventInstance footstep;
    private FMOD.Studio.ParameterInstance randomLoop;
    private FMOD.Studio.ParameterInstance end;// 0 = init, 1 = walk, 1.5 = end, 2 = echo
    private FMOD.Studio.ParameterInstance volume;
    [SerializeField] private float volumeSound = 100;// from 0 to 100

    private void Awake()
    {
        footstep = FMODUnity.RuntimeManager.CreateInstance(eventRef);
        footstep.getParameter("RandomFoot", out randomLoop);
        footstep.getParameter("end", out end);
        footstep.getParameter("Volume", out volume);
    }

    private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstep, transform, _myRigidBody);
	    footstep.start();
	    volume.setValue(volumeSound * (GameManager.Instance.VolumeMaster / 100));
	}

	private void FixedUpdate()
	{
		_myRigidBody.velocity = speed * (Vector2.right * _horizontalInput + Vector2.up * _verticalInput);
		if (_myRigidBody.velocity != Vector2.zero)
		{
			aim.transform.up = _myRigidBody.velocity;
		}
	}

	private void Update()
	{
		_horizontalInput = Input.GetAxis("Horizontal");
		_verticalInput = Input.GetAxis("Vertical");
		if (Input.GetButtonDown("Jump"))
		{
			//todo find a more elegant way to get the child of aim
			Collider2D hit = Physics2D.OverlapCircle(aim.transform.GetChild(0).transform.position, interactRadius);
			if (hit && hit.CompareTag("Breakable"))
			{
				hit.transform.GetComponent<Breakable>().Damage(damage);
			}
		}
	    if (_horizontalInput > 0.1 || _horizontalInput < -0.1 || _verticalInput > 0.1 || _verticalInput < -0.1)
	    {
	        end.setValue(1);
	    }
	    else if (_preHorizontalInput > 0.1 || _preHorizontalInput < -0.1 || _preVerticalInput > 0.1 || _preVerticalInput < -0.1)
	    {
	        end.setValue(1.5f);
	        randomLoop.setValue(Random.Range(1, 4));
        }

	    _preHorizontalInput = _horizontalInput;
	    _preVerticalInput = _verticalInput;
	}
}