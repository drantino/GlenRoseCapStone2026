using System;
using UnityEngine;

public class EmergencyVehicle : Vehicle
{
	protected override void PerformSquishedBehavior()
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

		// squished behavior 3: just keep driving forward
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}
}
