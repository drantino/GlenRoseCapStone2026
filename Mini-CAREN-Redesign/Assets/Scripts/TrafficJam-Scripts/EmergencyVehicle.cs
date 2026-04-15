using System;
using UnityEngine;

public class EmergencyVehicle : Vehicle
{
	protected override void PerformSquishedBehavior()
	{
		// squished behavior 3: just keep driving forward
		transform.Translate(Vector3.forward * gameManager.settings.CarSpeed * Time.deltaTime);
	}
}
