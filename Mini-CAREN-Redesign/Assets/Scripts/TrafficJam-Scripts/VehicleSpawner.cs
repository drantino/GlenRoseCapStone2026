using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
	[HideInInspector] public int currentCarsInLane = 0;
    
	[SerializeField] protected float spawnRateVarianceSec;
    [SerializeField] protected float minTimeBetweenVehicleSpawns;
	[SerializeField] protected float maxCarsInLane;
	//[SerializeField] protected float timeOffset; // subtracted from only the first timeUntilNextSpawn
    public TrafficJamGameManager gameManager;
    // Since we only have one car type/model, we only need one game object for the prefabs.
    
    [SerializeField] protected GameObject[] vehiclePrefabs;
    [SerializeField] protected GameObject[] longVehiclePrefabs;

    //public bool spawnLongVehicles; changed to setting.CarLengths
    public float longVehicleSpawnProbability;
    
    // TODO: Not needed but turning this into an enum would prevent errors from spelling mistakes.
    public string footTag;

    [SerializeField]
	protected float timeUntilNextSpawn;
    // Only the z axis matters for detourPos. This transform exists to easily manipulate where the z point is.
    [SerializeField] protected Transform detourPos;
    public float antiDoubleSpawnRayLength;
    protected int passedVehiclesTraveling;

    //[SerializeField] // uncomment for debugging
    protected List<Vehicle> VehicleList = new List<Vehicle>();

    // properties
    protected int vehiclesNotPassedIntersection => currentCarsInLane - passedVehiclesTraveling;
    
    void OnDrawGizmos()
    {
        // Gizmo to easily identify where the spawn point is.
        Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
        // Gizmo to identify where the anti-double-spawn raycast is. The box's dimensions do not rotate with the object, only being used with the x axis
        Gizmos.color = Color.grey;
        Gizmos.DrawCube(transform.position + (transform.forward * (antiDoubleSpawnRayLength / 2)), new Vector3(antiDoubleSpawnRayLength, 0.1f, 0.1f));
    }

    void Awake()
    {
        //TODO: refactor code so that this awake method isn't needed.
        currentCarsInLane = 0;
        passedVehiclesTraveling = 0;
    }

    void Start()
    {
        if (vehiclePrefabs.Length == 0)
        {
            gameObject.SetActive(false);
			throw new System.Exception("Vehicle spawner must have at least one car prefab.");
		}

		timeUntilNextSpawn = GetNextSpawnTime();
		// Temp statement to notify of spelling mistakes.
		if (footTag != "LeftShoe" && footTag != "RightShoe")
        {
            Debug.LogWarning("Variable 'footTag' was not given either 'LeftShoe' or 'RightShoe' as a value. Please correct before running the project again.");
        }
    }

    protected virtual void Update()
    {
        //timeUntilNextSpawn -= Time.deltaTime;
        //if (timeUntilNextSpawn < 0)
        //{
        //    timeUntilNextSpawn = GetNextSpawnTime();
        //    if (currentCarsInLane < maxCarsInLane && !Physics.Raycast(transform.position, transform.forward, antiDoubleSpawnRayLength))
        //    {
        //        if (footTag == "LeftShoe" && Random.Range(0, 101) < gameManager.settings.CarSpawnBias
        //            || footTag == "RightShoe" && Random.Range(0, 101) > gameManager.settings.CarSpawnBias)
        //        {
        //            SpawnCar();
        //        }
        //    }
        //}
    }

    protected virtual float GetNextSpawnTime()
    {
        return Random.Range( Mathf.Clamp(gameManager.settings.CarSpawnInterval - spawnRateVarianceSec, minTimeBetweenVehicleSpawns, 100), gameManager.settings.CarSpawnInterval + spawnRateVarianceSec);
	}

    /// <summary>
    /// Attempts to spawn a vehicle from this spawner
    /// </summary>
    /// <returns>If the car was spawned or not</returns>
    public virtual bool TrySpawnCar()
    {
        // if the lane is full, don't spawn
        if (currentCarsInLane >= maxCarsInLane)
            return false;

        // if there is a vehicle in the way, don't spawn
        if (Physics.Raycast(transform.position, transform.forward, antiDoubleSpawnRayLength))
            return false;

		// select random vehicle prefab
		GameObject prefab;
        bool isLong = false;
        if (gameManager.settings.CarLength >= 2 && Random.Range(0f, 1f) <= longVehicleSpawnProbability)
        {
            prefab = longVehiclePrefabs[Random.Range(0, longVehiclePrefabs.Length)];
            currentCarsInLane += 2;
            isLong = true;
        }   
		else
        {
            prefab = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)];
            currentCarsInLane++;
        }
        //             

        // spawn vehicle
        GameObject instantiatedVehicle = Instantiate(prefab, transform.position, transform.rotation);
        Vehicle instantiatedVehicleScript = instantiatedVehicle.GetComponent<Vehicle>();
        instantiatedVehicleScript.footTag = footTag;
        instantiatedVehicleScript.vehicleSpawner = this;
        instantiatedVehicleScript.detourZPos = detourPos.position.z;
        // detourEnabled will only be true if its the last car in lane.
        instantiatedVehicleScript.detourEnabled = currentCarsInLane >= maxCarsInLane && passedVehiclesTraveling == 0;
        instantiatedVehicleScript.moveSpeed = gameManager.settings.CarSpeed;
        instantiatedVehicleScript.gameManager = gameManager;
        instantiatedVehicleScript.isLong = isLong;
        //currentCarsInLane++;
        
        // add vehicle data to be referenced later
        VehicleList.Add(instantiatedVehicleScript);

        return true;
    }

    public virtual void RemovingVehicle(GameObject _Vehicle)
    {
        Vehicle vehicleComponent = _Vehicle.GetComponent<Vehicle>();
        if (vehicleComponent == null)
            throw new System.Exception($"No Vehicle component on vehicle '{_Vehicle.name}'");
        
        if (VehicleList.Remove(vehicleComponent))
        {
			Destroy(_Vehicle);
			
            passedVehiclesTraveling--;
            if (vehicleComponent.isLong)
                currentCarsInLane -= 2;
            else
                currentCarsInLane--;
			Debug.Log($"Removed vehicle '{_Vehicle.name}'");
        }
    }

    public void ResetVehicleList()
    {
        foreach (Vehicle vehi in VehicleList)
        {
            Destroy(vehi.gameObject);
        }
        VehicleList.Clear();
        currentCarsInLane = 0;
        passedVehiclesTraveling = 0;
    }

    // When the vehicle crosses the intersection, it will set the final vehicle's detourEnabled bool to false, 
    // due to it no longer being in the back of a four-car queue. 
    public virtual void VehicleCrossedIntersection()
    {
        passedVehiclesTraveling++;
        if (currentCarsInLane >= maxCarsInLane)
        {
            //VehicleList[(int)(maxCarsInLane - 1)].detourEnabled = false;
            //VehicleList[(int)(maxCarsInLane - 1)].StopAllCoroutines();
            VehicleList[VehicleList.Count - 1].detourEnabled = false;
            VehicleList[VehicleList.Count - 1].StopAllCoroutines();
        }
    }

    //[ContextMenu("Force Vehicle Spawn")]
    //public void ForceVehicleSpawn()
    //{
    //    if (currentCarsInLane < maxCarsInLane && !Physics.Raycast(transform.position, transform.forward, antiDoubleSpawnRayLength))
    //    {
    //        timeUntilNextSpawn = GetNextSpawnTime();
    //        TrySpawnCar();
    //    }
    //}

    // Method is currently unused due to it causing a ton of lag. Feel free to remove during final cleanup
    public bool IsLastInLine(GameObject vehicle)
    {
        // if (VehicleList.Count > maxCarsInLane)
        //     throw new System.Exception("There are more vehicles in the scene then the maximum.");

        // bool v = false;
        // if (VehicleList.Count == maxCarsInLane)
        // {
        //     v = VehicleList[(int)(maxCarsInLane - 1)] == vehicle;
        // }
        // return v;
        if (VehicleList.Count > maxCarsInLane)
            throw new System.Exception("There are more vehicles in the scene then the maximum.");

        bool v = false;
        if (VehicleList.Count == maxCarsInLane)
        {
            v = VehicleList[(int)(maxCarsInLane - 1)].gameObject == vehicle;
        }
        return v;
    }
}
