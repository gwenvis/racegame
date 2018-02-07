using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour {

	public GameObject fWheelLeft;
	public GameObject fWheelRight;
	public GameObject steeringWheel;
	public float rotateSpeed;
	public float maxRightTurn;
	public float maxLeftTurn;
	public float moveSpeed;

	void Start()
	{

	}

	void Update()
	{
		if (Input.anyKey)
		{
			CheckKey();
		}
	}

	void CheckKey()
	{
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
		{
			transform.GetComponent<CarHandler>().Drive();
		}

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			fWheelLeft.GetComponent<TurnWheel>().TurnLeft();
			fWheelRight.GetComponent<TurnWheel>().TurnLeft();
			transform.GetComponent<CarHandler>().Turn();
		}
	}
}
