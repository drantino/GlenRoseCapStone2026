using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class TrafficJamSessionData
{
	public string playerName;
	public DateTime dateTime;
	public List<TrafficJamRoundData> rounds = new List<TrafficJamRoundData>();
}