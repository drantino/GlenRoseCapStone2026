using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] protected TrafficJamGameManager gameManager;
	[HideInInspector] public float currentCarsInLane = 0;

	[SerializeField] protected float spawnRateSec;
	[SerializeField] protected float spawnRateVarianceSec;
	[SerializeField] protected float maxCarsInLane;
	[SerializeField] protected float timeOffset;

    // Since we only have one car type/model, we only need one game object for the prefabs.
    // TODO: Either have an array/list or a scriptable object to reference when we get many car models.
    [SerializeField] protected GameObject VehiclePrefab;
    // TODO: Not needed but turning this into an enum would prevent errors from spelling mistakes.
    public string footTag;

	protected float timeUntilNextSpawn;

    [SerializeField] protected List<GameObject> VehicleList = new List<GameObject>();

    // Gizmo to easily identify where the spawn point is.
    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
    }

    void Start()
    {
        currentCarsInLane = 0;
        timeUntilNextSpawn = Random.Range(spawnRateSec - spawnRateVarianceSec, spawnRateSec + spawnRateVarianceSec) - timeOffset;
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
            timeUntilNextSpawn = Random.Range(spawnRateSec - spawnRateVarianceSec, spawnRateSec + spawnRateVarianceSec);
            if (currentCarsInLane < maxCarsInLane)
            {
                SpawnCar();
            }
        }
    }

    protected virtual void SpawnCar()
    {
        GameObject instantiatedVehicle = Instantiate(VehiclePrefab, transform.position, transform.rotation);
        Vehicle instantiatedVehicleScript = instantiatedVehicle.GetComponent<Vehicle>();
        instantiatedVehicleScript.footTag = footTag;
        instantiatedVehicleScript.vehicleSpawner = this;
        currentCarsInLane++;

        //gameManager.AddToVechicleList(instantiatedVehicle);
        VehicleList.Add(instantiatedVehicle);
    }

    public void RemovingVechicle(GameObject Vehicle)
    {
        //gameManager.RemoveFromVechicleList(Vechicle);
        VehicleList.Remove(Vehicle);
        currentCarsInLane--;
    }

    public void ResetVehicleList()
    {
        foreach (GameObject vech in VehicleList)
        {
            Destroy(vech);
        }
        VehicleList.Clear();
        currentCarsInLane = 0;
    }
}
