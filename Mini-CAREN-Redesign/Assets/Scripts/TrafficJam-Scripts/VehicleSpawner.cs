using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
	[HideInInspector] public int currentCarsInLane = 0;
    
	[SerializeField] protected float maxCarsInLane;
    public TrafficJamGameManager gameManager;

    [SerializeField] protected GameObject[] vehiclePrefabs;
    [SerializeField] protected GameObject[] longVehiclePrefabs;

    public float longVehicleSpawnProbability;

    public string footTag;
    
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

		//timeUntilNextSpawn = GetNextSpawnTime();
		// Temp statement to notify of spelling mistakes.
		if (footTag != "LeftShoe" && footTag != "RightShoe")
        {
            Debug.LogWarning("Variable 'footTag' was not given either 'LeftShoe' or 'RightShoe' as a value. Please correct before running the project again.");
        }
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
            VehicleList[VehicleList.Count - 1].detourEnabled = false;
            VehicleList[VehicleList.Count - 1].StopAllCoroutines();
        }
    }
    
    // Method is currently unused due to it causing a ton of lag. Feel free to remove during final cleanup
    public bool IsLastInLine(GameObject vehicle)
    {
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
