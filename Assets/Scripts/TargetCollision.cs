using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetCollision : MonoBehaviour {

	[SerializeField] private GameObject wall;
	[SerializeField] private GameObject train;

	private Vector3 trainStartPos;

	public Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		trainStartPos = train.transform.GetComponent<TrainMovement>().startPos;
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.transform.name == "Train")
		{
			if (transform.position == startPos)
			{
				transform.position = trainStartPos;
			}
			else if (transform.position == trainStartPos)
			{
				transform.position = startPos;
			}

			train.GetComponent<NavMeshAgent>().SetDestination(transform.position);
		}
	}
}
