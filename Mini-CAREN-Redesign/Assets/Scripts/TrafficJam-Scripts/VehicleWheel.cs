using UnityEngine;

public class VehicleWheel : MonoBehaviour
{
	[SerializeField] private float radius;
	[SerializeField] private Vector3 axisOfRotation;

	public void RotateByDistance(float moveDistance)
	{
		// C = 2 * PI * r
		// d/C = a/360
		// a = 360 * d/C
		// a = 360 * d/(2 * PI * r)

		float angle = (360 * moveDistance) / (2 * Mathf.PI * radius);
		transform.Rotate(axisOfRotation, angle);
	}
}
