using UnityEngine;

public class VehicleWheel : MonoBehaviour
{
	[SerializeField] private float radius;
	[SerializeField] private Vector3 axisOfRotation;

	public void RotateByDistance(float moveDistance)
	{
		float angle = (360 * moveDistance) / (2 * Mathf.PI * radius);
		transform.Rotate(axisOfRotation, angle);
	}
}
