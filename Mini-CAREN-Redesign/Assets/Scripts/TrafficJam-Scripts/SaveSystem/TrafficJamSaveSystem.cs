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

	public static void SetPlayerName(string playerName)
	{
		instance.sessionData.playerName = playerName;
	}

	[ContextMenu("Save Session")]
	public void SaveSessionContextMenuAsset()
	{
		SaveSessionToDisk();
	}

	public static void SaveSessionToDisk()
	{
		// get time
		DateTime time = DateTime.Now;

		string fileName = $"{instance.sessionData.playerName}_{time.Month}-{time.Day}-{time.Year}_{time.Hour}-{time.Minute}-{time.Second}";
		string filePath = $"{Application.dataPath}/SessionSaves/{fileName}.txt";
		string json = JsonUtility.ToJson(instance.sessionData, true);
		File.WriteAllText(filePath, json);

		Debug.Log($"Profile saved to location: '{filePath}'");
	}
}
