using UnityEngine;

public class VehicleDespawner : MonoBehaviour
{
	[SerializeField] private VehicleSpawner vehicleSpawner;
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Vehicle")
		{
			Destroy(other.transform.parent.gameObject);
			vehicleSpawner.currentCarsInLane--;
		}
	}
}
