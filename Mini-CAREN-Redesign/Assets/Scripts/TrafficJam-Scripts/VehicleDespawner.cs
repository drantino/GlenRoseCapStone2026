using UnityEngine;

public class VehicleDespawner : MonoBehaviour
{
	[SerializeField] private VehicleSpawner vehicleSpawner;
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Vehicle")
		{
			//vehicleSpawner.RemovingVechicle(other.transform.parent.gameObject);
			Destroy(other.transform.parent.gameObject);
		}
	}
}
