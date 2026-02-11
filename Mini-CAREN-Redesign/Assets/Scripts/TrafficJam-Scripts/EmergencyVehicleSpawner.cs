using UnityEngine;

public class EmergencyVehicleSpawner : VehicleSpawner
{
	[SerializeField] private Transform leftSpawnPosition;
	[SerializeField] private Transform rightSpawnPosition;
	[SerializeField] private TrafficLight trafficLight1;
	[SerializeField] private TrafficLight trafficLight2;

	private bool spawnCarOnLeft = false;

	protected override void Update()
	{
		base.Update();

		// if there is an emergency vehicle, stop traffic.
		if (currentCarsInLane > 0)
		{
			if (!trafficLight1.redLightOn) trafficLight1.TurnOnRedLight();
			if (!trafficLight2.redLightOn) trafficLight2.TurnOnRedLight();
		}
		else
		{
			if (trafficLight1.redLightOn) trafficLight1.TurnOnGreenLight();
			if (trafficLight2.redLightOn) trafficLight2.TurnOnGreenLight();
		}
	}

	protected override void SpawnCar()
	{
		// choose randomly wether to spawn this vehicle on the left lane, or right lane.
		spawnCarOnLeft = Random.value > 0.5;

		string tag = string.Empty;
		Vector3 spawnPos = Vector3.zero;
		if (spawnCarOnLeft)
		{
			tag = "LeftShoe";
			spawnPos = leftSpawnPosition.position;
		} 
		else
		{
			tag = "RightShoe";
			spawnPos = rightSpawnPosition.position;
		}

		// spawn vehicle
		GameObject instantiatedVehicle = Instantiate(VehiclePrefab, spawnPos, transform.rotation);
		Vehicle instantiatedVehicleScript = instantiatedVehicle.GetComponent<Vehicle>();
		instantiatedVehicleScript.footTag = tag;
		instantiatedVehicleScript.vehicleSpawner = this;
		currentCarsInLane++;
	}
}
