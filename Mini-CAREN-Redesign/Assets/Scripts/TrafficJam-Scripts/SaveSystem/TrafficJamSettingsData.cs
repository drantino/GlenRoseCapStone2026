using System;
using UnityEngine;

[Serializable]
public class TrafficJamSettingsData
{
	public float heightThreshold;
	public float carSpeed;
	public float carSpawnInterval;
	public float carLength;
	public bool carDetour;
	public float emergencyVehicleSideBias;
	public bool emergencyVehicleActive;
}
