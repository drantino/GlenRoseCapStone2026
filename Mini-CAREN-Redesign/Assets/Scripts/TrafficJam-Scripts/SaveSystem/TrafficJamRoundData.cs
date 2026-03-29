using System;
using UnityEngine;

[Serializable]
public class TrafficJamRoundData
{
	public float roundLength;

	public int leftFootPassed;
	public int leftFootSquished;
	public int leftFootDetoured;

	public int rightFootPassed;
	public int rightFootSquished;
	public int rightFootDetoured;

	public TrafficJamSettingsData settingsData = new TrafficJamSettingsData();
}
