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
	private Rigidbody2D _myRigidBody;

	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
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
	}
}