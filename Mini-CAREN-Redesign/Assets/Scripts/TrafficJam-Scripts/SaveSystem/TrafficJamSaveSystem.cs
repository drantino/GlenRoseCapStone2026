using System;
using System.IO;
using UnityEngine;

public class TrafficJamSaveSystem : MonoBehaviour
{
	private static TrafficJamSaveSystem instance;

	[SerializeField] private TrafficJamSessionData sessionData;

	private void Awake()
	{
		instance = this;
		sessionData = new TrafficJamSessionData();
	}

	private void OnDestroy()
	{
		instance = null;
	}

	public static void AddRoundData(TrafficJamRoundData trafficJamRoundData)
	{
		instance.sessionData.rounds.Add(trafficJamRoundData);
	}

	[ContextMenu("Save Session")]
	public void SaveSessionToDisk()
	{
		// get time
		DateTime time = DateTime.Now;
		
		string fileName = $"{instance.sessionData.playerName}_{time.ToString()}";
		string filePath = $"{Application.dataPath}/sessions/{fileName}";
		string json = JsonUtility.ToJson(instance.sessionData, true);
		File.WriteAllText(filePath, json);

		Debug.Log($"Profile saved to location: '{filePath}'");
	}
}
