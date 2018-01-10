using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrainMovement : MonoBehaviour {

	[SerializeField] float moveSpeed;
	[SerializeField] Transform target;

	private NavMeshAgent navAgent;

	public Vector3 startPos;

	// Use this for initialization
	void Start ()
	{
		startPos = transform.position;
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.speed = moveSpeed;

		if (target)
		{
			navAgent.SetDestination(target.position);
		}
		else
		{
			target = GetComponent<Transform>();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
	}
}
