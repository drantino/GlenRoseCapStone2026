using UnityEngine;

public class VehicleDespawner : MonoBehaviour
{
	[SerializeField] private VehicleSpawner vehicleSpawner;
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Vehicle")
		{
			//Debug.Log($"Vehicle '{other.transform.parent.name}' has entered vehicle despawner.");
			vehicleSpawner.RemovingVehicle(other.transform.parent.gameObject);
		}
	}
}
