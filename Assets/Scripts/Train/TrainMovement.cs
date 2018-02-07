using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class TrainMovement : MonoBehaviour {
	
	[SerializeField] float moveSpeed;
	[SerializeField] string easeType;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.D))
		{
			Move();
		}
	}
	
	void Move()
	{
		iTween.MoveTo(gameObject, iTween.Hash(
			"path", iTweenPath.GetPath("Rails"),
			"orienttopath", true,
			"speed", moveSpeed,
			"easetype", easeType
		));
	}

	
}
