using UnityEngine;

public class EmergencyVehicleSpawner : VehicleSpawner
{
	[SerializeField] protected float spawnRateSec;

	[SerializeField] private Transform leftSpawnPosition;
	[SerializeField] private Transform rightSpawnPosition;
	[SerializeField] private TrafficLight trafficLight1;
	[SerializeField] private TrafficLight trafficLight2;
	[SerializeField] private GameObject vehicleStopper1;
	[SerializeField] private GameObject vehicleStopper2;
	[SerializeField] private TrafficGate[] trafficGates;

	public AudioLoop sirenLoop;
	public float sirenFadeTime;

	private bool spawnCarOnLeft = false;

	protected override void Update()
	{
		
        

        // if there is an emergency vehicle, stop traffic.
        if (vehiclesNotPassedIntersection > 0)
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

	protected override float GetNextSpawnTime()
	{
		return Random.Range( Mathf.Clamp(spawnRateSec - spawnRateVarianceSec, minTimeBetweenVehicleSpawns, 100), spawnRateSec + spawnRateVarianceSec);
	}
	public override bool TrySpawnCar()
	{
		// if the lane is full, don't spawn
		if (currentCarsInLane >= maxCarsInLane)
			return false;

		// if there is a vehicle in the way, don't spawn
		if (Physics.Raycast(transform.position, transform.forward, antiDoubleSpawnRayLength))
			return false;

		// choose randomly wether to spawn this vehicle on the left lane, or right lane.
		spawnCarOnLeft = Random.value > gameManager.settings.EmergencyVehicleBias/100;

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

		// select random vehicle prefab
		GameObject prefab = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)];

		// spawn vehicle
		GameObject instantiatedVehicle = Instantiate(prefab, spawnPos, transform.rotation);
		Vehicle instantiatedVehicleScript = instantiatedVehicle.GetComponent<Vehicle>();
		instantiatedVehicleScript.footTag = tag;
		instantiatedVehicleScript.vehicleSpawner = this;
		instantiatedVehicleScript.detourEnabled = false;
		instantiatedVehicleScript.detourZPos = 0;
		instantiatedVehicleScript.gameManager = gameManager;
		
		currentCarsInLane++;

		//gameManager.AddToVechicleList(instantiatedVehicle);
		VehicleList.Add(instantiatedVehicleScript);

		sirenLoop.FadeIn(sirenFadeTime);

		return true;
	}

	public override void VehicleCrossedIntersection()
	{
		base.VehicleCrossedIntersection();

		sirenLoop.FadeOut(sirenFadeTime);
	}
}
