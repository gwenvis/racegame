using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	[SerializeField] float squash;
	[SerializeField] Vector3 acceleration;

	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Bounce()
	{
		
	}

	private void OnCollisionEnter(Collision col)
	{
		rb.velocity += acceleration;
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / squash, transform.localScale.z);
	}

	private void OnCollisionExit(Collision col)
	{
		transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
	}
}
