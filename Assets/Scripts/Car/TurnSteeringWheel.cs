using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSteeringWheel : MonoBehaviour {

	GameObject car;
	//Quaternion startRotation;
	float rotateSpeed;
	float maxRightTurn;
	float maxLeftTurn;

	void Awake()
	{
		car = transform.parent.transform.parent.gameObject;
	}

	void Start ()
	{
		//startRotation = transform.localRotation;
		rotateSpeed = car.GetComponent<CarInput>().rotateSpeed;
		maxRightTurn = car.GetComponent<CarInput>().maxRightTurn;
		maxLeftTurn = car.GetComponent<CarInput>().maxLeftTurn;
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
				transform.localRotation = new Quaternion(transform.localRotation.x + rotateSpeed * Time.deltaTime, transform.localRotation.y + rotateSpeed * Time.deltaTime, transform.localRotation.z, transform.localRotation.w);
			}
		}
		else if (Input.GetKey(KeyCode.A))
		{
			if (transform.localRotation.x >= maxLeftTurn)
			{
				// Turn left
				transform.localRotation = new Quaternion(transform.localRotation.x - rotateSpeed * Time.deltaTime, transform.localRotation.y - rotateSpeed * Time.deltaTime, transform.localRotation.z, transform.localRotation.w);
			}
		}
	}
}
