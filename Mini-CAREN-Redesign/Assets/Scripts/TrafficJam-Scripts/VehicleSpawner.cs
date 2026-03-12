using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
	[HideInInspector] public float currentCarsInLane = 0;

	[SerializeField] protected float spawnRateSec;
	[SerializeField] protected float spawnRateVarianceSec;
	[SerializeField] protected float maxCarsInLane;
	[SerializeField] protected float timeOffset;
    public TrafficJamGameManager gameManager;
    // Since we only have one car type/model, we only need one game object for the prefabs.
    [SerializeField] protected GameObject[] VehiclePrefabs;
    
    // TODO: Not needed but turning this into an enum would prevent errors from spelling mistakes.
    public string footTag;

	protected float timeUntilNextSpawn;
    // Only the z axis matters for detourPos. This transform exists to easily manipulate where the z point is.
    [SerializeField] protected Transform detourPos;
    public bool vehiclesDetour = false;

    protected List<Vehicle> VehicleList = new List<Vehicle>();

    // Gizmo to easily identify where the spawn point is.
    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
    }

    void Start()
    {
        if (VehiclePrefabs.Length == 0)
        {
            gameObject.SetActive(false);
			throw new System.Exception("Vehicle spawner must have at least one car prefab.");
		}
            

        currentCarsInLane = 0;
        timeUntilNextSpawn = Random.Range(gameManager.settings.CarSpawnInterval - spawnRateVarianceSec, gameManager.settings.CarSpawnInterval + spawnRateVarianceSec) - timeOffset;
        // Temp statement to notify of spelling mistakes.
        if (footTag != "LeftShoe" && footTag != "RightShoe")
        {
            Debug.LogWarning("Variable 'footTag' was not given either 'LeftShoe' or 'RightShoe' as a value. Please correct before running the project again.");
        }
    }

    protected virtual void Update()
    {        
        timeUntilNextSpawn -= Time.deltaTime;
        if (timeUntilNextSpawn < 0)
        {
            timeUntilNextSpawn = Random.Range(gameManager.settings.CarSpawnInterval - spawnRateVarianceSec, gameManager.settings.CarSpawnInterval + spawnRateVarianceSec);
            if (currentCarsInLane < maxCarsInLane)
            {
                SpawnCar();
            }
        }
    }

    protected virtual void SpawnCar()
    {
        // select random vehicle prefab
        GameObject prefab = VehiclePrefabs[Random.Range(0, VehiclePrefabs.Length)];
        
        // spawn vehicle
        GameObject instantiatedVehicle = Instantiate(prefab, transform.position, transform.rotation);
        Vehicle instantiatedVehicleScript = instantiatedVehicle.GetComponent<Vehicle>();
        instantiatedVehicleScript.footTag = footTag;
        instantiatedVehicleScript.vehicleSpawner = this;
        instantiatedVehicleScript.detourZPos = detourPos.position.z;
        instantiatedVehicleScript.detourEnabled = gameManager.settings.CarDetour;
        instantiatedVehicleScript.moveSpeed = gameManager.settings.CarSpeed;
        currentCarsInLane++;

        // add vehicle date to be referenced later
        VehicleList.Add(instantiatedVehicleScript);

    }

    public void RemovingVechicle(GameObject _Vehicle)
    {
        //gameManager.RemoveFromVechicleList(Vechicle);
        //VehicleList.Remove(Vehicle);
        for (int i = 0; i < VehicleList.Count; i++)
        {
            if (VehicleList[i].gameObject == _Vehicle)
            {
                VehicleList.RemoveAt(i);
                i = VehicleList.Count + 1;
            }
        }
        currentCarsInLane--;
    }

    public void ResetVehicleList()
    {
        // foreach (GameObject vech in VehicleList)
        // {
        //     Destroy(vech);
        // }
        // VehicleList.Clear();

        foreach (Vehicle vehi in VehicleList)
        {
            Destroy(vehi.gameObject);
        }
        VehicleList.Clear();
        currentCarsInLane = 0;
    }

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
