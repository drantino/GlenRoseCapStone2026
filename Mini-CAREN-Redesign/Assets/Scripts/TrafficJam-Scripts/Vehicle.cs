using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public string footTag; // this determines what foot the vehicle will stop infont of, and can be stomped by
	[HideInInspector] public VehicleSpawner vehicleSpawner;

	[SerializeField] private GameObject vehicleModel;
	[SerializeField] private GameObject vehicleSquishedModel;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float vehicleStopDistance;
	[SerializeField] private float timeUntilDespawnAfterSquish;
	
	private bool squished = false;

	private void Start()
	{
		vehicleModel.SetActive(true);
		vehicleSquishedModel.SetActive(false);

		if (vehicleModel == null)
			throw new System.Exception("the vehicle model is null");
		if (vehicleSquishedModel == null)
			throw new System.Exception("the vehicle squished model is null");
	}

	private void Update()
	{
		// check if there is an object infront of the vehicle
		Physics.BoxCast(transform.position, new Vector3(0.2f, 0.2f, 0.2f), transform.forward, out RaycastHit hit, Quaternion.identity, vehicleStopDistance);
		bool objectInfront = hit.transform != null && (hit.transform.CompareTag(footTag) || hit.transform.CompareTag("Vehicle"));

		if (!objectInfront && !squished)
		{
			// move
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}

		if (squished)
		{
			// TODO: do more complex squished movement (for now just despawns after a few seconds)
			timeUntilDespawnAfterSquish -= Time.deltaTime;
			if (timeUntilDespawnAfterSquish < 0)
			{
				vehicleSpawner.currentCarsInLane--;
				Destroy(gameObject);
				return;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		string tag = other.tag;
		if (other.tag == footTag)
			Squish();
	}

	private void Squish()
	{
		squished = true;
		vehicleModel.SetActive(false);
		vehicleSquishedModel?.SetActive(true);
	}
}
