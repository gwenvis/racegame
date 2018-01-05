using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour {

	GameObject car;
	float rotateSpeed;
	float maxRightTurn;
	float maxLeftTurn;
	float moveSpeed;
	Rigidbody rb;

	void Start ()
	{
		rotateSpeed = transform.GetComponent<CarInput>().rotateSpeed;
		maxRightTurn = transform.GetComponent<CarInput>().maxRightTurn;
		maxLeftTurn = transform.GetComponent<CarInput>().maxLeftTurn;
		moveSpeed = transform.GetComponent<CarInput>().moveSpeed;
	}
	
	void Update ()
	{
		
	}

	public void Turn()
	{
		if (Input.GetKey(KeyCode.D))
		{
			if (transform.localRotation.x <= maxRightTurn)
			{
				// Turn Right
				transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y + rotateSpeed * Time.deltaTime, transform.localRotation.z, transform.localRotation.w);
			}
		}
		else if (Input.GetKey(KeyCode.A))
		{
			if (transform.localRotation.x >= maxLeftTurn)
			{
				// Turn left
				transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y - rotateSpeed * Time.deltaTime, transform.localRotation.z, transform.localRotation.w);
			}
		}
	}

	public void Drive()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed * Time.deltaTime);
	}
}
