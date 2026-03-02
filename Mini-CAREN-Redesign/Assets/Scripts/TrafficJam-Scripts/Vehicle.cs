using System;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public string footTag; // this determines what foot the vehicle will stop infont of, and can be stomped by
	[HideInInspector] public VehicleSpawner vehicleSpawner;

	[SerializeField] public float moveSpeed;

	[SerializeField] protected GameObject vehicleModel;
	[SerializeField] protected GameObject vehicleSquishedModel;
	[SerializeField] protected float turnMoveSpeed;
	[SerializeField] protected float vehicleStopDistance;
	[SerializeField] protected float timeUntilDespawnAfterSquish;
	[SerializeField] protected float squishedLaneDistance;

	public bool squished = false;
	protected float originalZPos;
	protected float originalTimeUntilDespawnAfterSquish;
	private void Start()
	{
		if (vehicleModel == null)
			throw new System.Exception("the vehicle model is null");
		if (vehicleSquishedModel == null)
			throw new System.Exception("the vehicle squished model is null");

		vehicleModel.SetActive(true);
		vehicleSquishedModel.SetActive(false);

		originalZPos = transform.position.z;

		originalTimeUntilDespawnAfterSquish = timeUntilDespawnAfterSquish;
	}

	private void Update()
	{
		// check if there is an object infront of the vehicle
		//Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, out RaycastHit hit, Quaternion.identity, vehicleStopDistance);

		//bool objectInfront = hit.transform != null && (
		//	hit.transform.CompareTag(footTag) || hit.transform.CompareTag("Vehicle") || hit.transform.CompareTag("VehicleStopper"));

		RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, Quaternion.identity, vehicleStopDistance);
		bool objectInfront = false;

		foreach (RaycastHit hit in hits)
		{
			if (hit.transform != null && hit.transform.gameObject != gameObject &&
				(hit.transform.CompareTag(footTag) || hit.transform.CompareTag("Vehicle") || hit.transform.CompareTag("VehicleStopper")))
			{
				objectInfront = true;
				break;
			}
		}

		if (!objectInfront && !squished)
		{
			// move
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}

		if (squished)
		{
			PerformSquishedBehavior();
		}
	}

	protected virtual void PerformSquishedBehavior()
	{
		// squished behavior 1: despawn after a couple seconds
		//if (timeUntilDespawnAfterSquish < originalTimeUntilDespawnAfterSquish / 2f)
		//	vehicleSquishedModel.SetActive(Math.Sin(timeUntilDespawnAfterSquish * 40) > 0); // do flashing animation

		//timeUntilDespawnAfterSquish -= Time.deltaTime;
		//if (timeUntilDespawnAfterSquish < 0)
		//{
		//	// despawn vehicle
		//	vehicleSpawner.currentCarsInLane--;
		//	Destroy(gameObject);
		//	return;
		//}



		// squished behavior 2: move into "squished" lane
		if (transform.forward.x > 0)
		{
			if (transform.position.z - originalZPos > squishedLaneDistance)
				transform.Translate(Vector3.right * turnMoveSpeed * Time.deltaTime);
		}
		else if (transform.position.z - originalZPos < -squishedLaneDistance)
		{
			transform.Translate(Vector3.right * turnMoveSpeed * Time.deltaTime);
		}
			

		// move forward
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);



		// squished behavior 3: just keep driving forward
		//transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
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
