using UnityEngine;

public class EmergencyVehicleSpawner : VehicleSpawner
{
	[SerializeField] private Transform leftSpawnPosition;
	[SerializeField] private Transform rightSpawnPosition;
	[SerializeField] private TrafficLight trafficLight1;
	[SerializeField] private TrafficLight trafficLight2;
	//[SerializeField] private TrafficGate trafficGate1;
	//[SerializeField] private TrafficGate trafficGate2;
	[SerializeField] private GameObject vehicleStopper1;
	[SerializeField] private GameObject vehicleStopper2;
	[SerializeField] private TrafficGate[] trafficGates;

	private bool spawnCarOnLeft = false;

	protected override void Update()
	{
		base.Update();

		// if there is an emergency vehicle, stop traffic.
		if (currentCarsInLane > 0)
		{
			// stop cars
			vehicleStopper1.SetActive(true);
			vehicleStopper2.SetActive(true);

			// update traffic lights
			if (trafficLight1 != null && !trafficLight1.redLightOn) trafficLight1.TurnOnRedLight();
			if (trafficLight2 != null && !trafficLight2.redLightOn) trafficLight2.TurnOnRedLight();

			// update gates
			foreach (TrafficGate gate in trafficGates)
				gate.Close();
		}
		else
		{
			// let cars pass
			vehicleStopper1.SetActive(false);
			vehicleStopper2.SetActive(false);

			// update traffic lights
			if (trafficLight1 != null && trafficLight1.redLightOn) trafficLight1.TurnOnGreenLight();
			if (trafficLight2 != null && trafficLight2.redLightOn) trafficLight2.TurnOnGreenLight();

			// update gates
			foreach (TrafficGate gate in trafficGates)
				gate.Open();
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

		//gameManager.AddToVechicleList(instantiatedVehicle);
		VehicleList.Add(instantiatedVehicle);
	}
}
