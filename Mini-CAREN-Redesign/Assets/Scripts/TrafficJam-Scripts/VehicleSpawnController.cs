using UnityEngine;

public class VehicleSpawnController : MonoBehaviour
{
	public TrafficJamGameManager gameManager;
	public VehicleSpawner leftSpawner;
	public VehicleSpawner rightSpawner;
	public VehicleSpawner emergencySpawner;

	public bool spawningEnabled;

	[SerializeField] private float emergencyVehicleSpawnRate;
	[SerializeField] private float spawnRateVarianceSec;
	[SerializeField] private float minTimeBetweenSpawns;
	
	[SerializeField] private float emergencySpawnRateVarianceSec;
	[SerializeField] private float emergencyMinTimeBetweenSpawns;

	[SerializeField]
	private float timeUntilNextVehicleSpawn;

	[SerializeField]
	private float timeUntilNextEmergencyVehicleSpawn;

	private void Awake()
	{
		if (gameManager == null)
			throw new System.Exception("Game Manager is null");
		if (leftSpawner == null)
			throw new System.Exception("Left Spawner is null");
		if (rightSpawner == null)
			throw new System.Exception("Right Spawner is null");
		if (emergencySpawner == null)
			throw new System.Exception("Emergency Spawner is null");
	}

	private void Start()
	{
		timeUntilNextVehicleSpawn = GetNextSpawnTime();
		timeUntilNextEmergencyVehicleSpawn = GetNextEmergencySpawnTime();
	}

	private void Update()
	{
		if (!spawningEnabled) return;

		timeUntilNextVehicleSpawn -= Time.deltaTime;
		timeUntilNextEmergencyVehicleSpawn -= Time.deltaTime;

		if (timeUntilNextVehicleSpawn < 0)
		{
			timeUntilNextVehicleSpawn = GetNextSpawnTime();
			TrySpawnLeftOrRightCar();
		}

		if (timeUntilNextEmergencyVehicleSpawn < 0)
		{
			timeUntilNextEmergencyVehicleSpawn = GetNextEmergencySpawnTime();
			emergencySpawner.TrySpawnCar();
		}
	}

	private float GetNextSpawnTime()
	{
		return Random.Range(Mathf.Clamp(gameManager.settings.CarSpawnInterval - spawnRateVarianceSec, minTimeBetweenSpawns, 100), gameManager.settings.CarSpawnInterval + spawnRateVarianceSec);
	}

	private float GetNextEmergencySpawnTime()
	{
		return Random.Range(Mathf.Clamp(emergencyVehicleSpawnRate - emergencySpawnRateVarianceSec, minTimeBetweenSpawns, 100), emergencyVehicleSpawnRate + emergencySpawnRateVarianceSec);
	}

	private void TrySpawnLeftOrRightCar()
	{
		float randomValue = Random.Range(1.0f, 100.00f);
		if (randomValue < gameManager.settings.CarSpawnBias)
			leftSpawner.TrySpawnCar();
		else
			rightSpawner.TrySpawnCar();
	}

	public void ForceVehicleSpawn()
	{
		timeUntilNextVehicleSpawn = GetNextSpawnTime();
		TrySpawnLeftOrRightCar();
	}
}
