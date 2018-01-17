using UnityEngine;

[ExecuteInEditMode]
public class PointToCircle : MonoBehaviour
{
	public Transform track; // the object where it should get the point from. 
	
	private float radius = 1f / 2; // radius of the circle
	private Vector3 point; 
	
	void Update ()
	{
		point = ClosestPointToCircleEdge(track.transform.position) + transform.position;
	}

	Vector3 ClosestPointToCircleEdge(Vector3 track)
	{
        // map track pos to local space
        var controllerpos = transform.InverseTransformPoint(track);
        // also map current pos to local space.
        var localCirclePos = transform.InverseTransformPoint(transform.position);
        controllerpos = controllerpos - localCirclePos;

        var a = controllerpos.x - localCirclePos.x;
        var b = controllerpos.z - localCirclePos.z;
        var mag = Mathf.Sqrt(a * a + b * b);

        // the formula is 
        // C(x,y) = A(x,y) + r * B(x,y) - A(x,y) / √ (Bx - Ax)^2 + (By - Ay)^2
        float cx = localCirclePos.x + radius * a / mag;
        Debug.Log(radius);
        float cz = localCirclePos.z + radius * b / mag;

        return transform.TransformVector(new Vector3(cx, 0, cz));
    }

	void OnDrawGizmos()
	{
		Gizmos.color = new UnityEngine.Color(0.5f,0.5f,0.8f,0.5f);
        Gizmos.DrawSphere(point, 0.1f);
	}
}
