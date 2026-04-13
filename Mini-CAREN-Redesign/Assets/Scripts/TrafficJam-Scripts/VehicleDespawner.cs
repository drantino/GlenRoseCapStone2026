using UnityEngine;

public class VehicleDespawner : MonoBehaviour
{
	[SerializeField] private VehicleSpawner vehicleSpawner;
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Vehicle")
		{
			vehicleSpawner.RemovingVehicle(other.transform.parent.gameObject);
		}
	}
}
