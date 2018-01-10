using UnityEngine;

[ExecuteInEditMode]
public class PointToCircle : MonoBehaviour
{
	public Transform track; // the object where it should get the point from. 
	
	private float radius; // radius of the circle
	private Vector3 point; 
	
	void Start ()
	{
		radius = transform.localScale.x / 2;
	}
	
	void Update ()
	{
		point = ClosestPointToCircleEdge(track.transform.position) + transform.position;
	}

	Vector3 ClosestPointToCircleEdge(Vector3 track)
	{

	}

	void OnDrawGizmos()
	{
		Gizmos.color = new UnityEngine.Color(0.5f,0.5f,0.8f,0.5f);
		Gizmos.DrawCube(point, new Vector3(0.2f, 0.2f, 0.2f));
	}
}
