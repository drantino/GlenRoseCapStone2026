using System.Collections;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public string footTag; // this determines what foot the vehicle will stop infont of, and can be stomped by
	[HideInInspector] public VehicleSpawner vehicleSpawner;
	public TrafficJamGameManager gameManager;

	[SerializeField] public float moveSpeed;
	[SerializeField] public float speedMultiplier;

	[SerializeField] protected GameObject vehicleModel;
	[SerializeField] protected GameObject vehicleSquishedModel;
	[SerializeField] protected VehicleWheel[] wheels;
	[SerializeField] protected Transform boxCastStartPosition;
	[SerializeField] protected float turnSpeedMultiplier;
	[SerializeField] protected float vehicleStopDistance;
	[SerializeField] protected float raycastStartDistance;
	[SerializeField] protected float timeUntilDespawnAfterSquish;
	[SerializeField] protected float squishedLaneDistance;

	[SerializeField] protected float detourCountdownSec;
	[SerializeField] // temp
	private bool detourCountdownRunning;
	[SerializeField] private float originalYRotation;
	public float detourZPos;
	public bool detourEnabled;

	[SerializeField] private float deformationImpulse;

	public bool squished = false, detouring = false, isLong = false;
	protected float originalZPos;
	protected float originalTimeUntilDespawnAfterSquish;

	private RMD_Deformation deformationScript;

	protected virtual void Start()
	{
		if (vehicleModel == null)
			throw new System.Exception("the vehicle model is null");
		if (vehicleSquishedModel == null)
			throw new System.Exception("the vehicle squished model is null");
		
		vehicleModel.SetActive(true);
		vehicleSquishedModel.SetActive(false);

		originalZPos = transform.position.z;

		originalTimeUntilDespawnAfterSquish = timeUntilDespawnAfterSquish;

		originalYRotation = transform.eulerAngles.y;

		deformationScript = GetComponent<RMD_Deformation>();
	}

	private void Update()
	{
		moveSpeed = gameManager.settings.CarSpeed;
		// check if there is an object infront of the vehicle
		//Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, out RaycastHit hit, Quaternion.identity, vehicleStopDistance);

		//bool objectInfront = hit.transform != null && (
		//	hit.transform.CompareTag(footTag) || hit.transform.CompareTag("Vehicle") || hit.transform.CompareTag("VehicleStopper"));

		//RaycastHit[] hits = Physics.BoxCastAll(transform.position + transform.forward * raycastStartDistance, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, Quaternion.identity, vehicleStopDistance);
		RaycastHit[] hits = Physics.BoxCastAll(boxCastStartPosition.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, Quaternion.identity, vehicleStopDistance);
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
			float distance = moveSpeed * speedMultiplier * Time.deltaTime;
			transform.Translate(Vector3.forward * distance);
			foreach (VehicleWheel wheel in wheels)
			{
				wheel.RotateByDistance(distance);
			}
		}

		if (squished)
		{
			PerformSquishedBehavior();
		}

		if (detourEnabled && objectInfront && !detourCountdownRunning && gameManager.settings.CarDetour)
		{
			detourCountdownRunning = true;
			StartCoroutine(DetourCountdown());
		} 
		else if (!objectInfront)
		{
			detourCountdownRunning = false;
			StopAllCoroutines();
		}

		if (Mathf.Abs(transform.position.z) > Mathf.Abs(detourZPos) && !squished)
		{
			transform.eulerAngles = new Vector3(0, -originalYRotation, 0);
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
				transform.Translate(Vector3.right * moveSpeed * turnSpeedMultiplier * Time.deltaTime);
		}
		else if (transform.position.z - originalZPos < -squishedLaneDistance)
		{
			transform.Translate(Vector3.right * moveSpeed * turnSpeedMultiplier * Time.deltaTime);
		}
			

		// move forward
		transform.Translate(Vector3.forward * moveSpeed * speedMultiplier * Time.deltaTime);



		// squished behavior 3: just keep driving forward
		//transform.Translate(Vector3.forward * moveSpeed * speedMultiplier * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(footTag))
			Squish();
		//else if (other.CompareTag("Vehicle"))
		//{
		//	// this should never happen. But if it does, it is possible that the two cars will both stop, breaking the game.
		//	// to fix this, we remove this vehicle from the scene
		//	vehicleSpawner.RemovingVehicle(gameObject);
		//	Destroy(gameObject);
		//}
	}

	private void Squish()
	{
		AudioPlayer.Play(Sound.CarSquish);
		squished = true;
		vehicleModel.SetActive(false);
		vehicleSquishedModel?.SetActive(true);

		if (deformationScript != null)
			deformationScript.DamageMesh(deformationImpulse);
	}

	private IEnumerator DetourCountdown()
	{
		yield return new WaitForSeconds(4);
		if (gameManager.settings.CarDetour)
		{
			detourCountdownRunning = false;
			detouring = true;
			transform.Rotate(0, 90, 0); 
		}
	}
}

