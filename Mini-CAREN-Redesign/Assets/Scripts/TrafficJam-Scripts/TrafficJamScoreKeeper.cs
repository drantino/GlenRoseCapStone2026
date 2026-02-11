using UnityEngine;

public class TrafficJamScoreKeeper : MonoBehaviour
{
	//private static TrafficJamScoreKeeper instance;
	public static int rightCarsSpawned, leftCarsSpawned, rightCarsPassed, leftCarsPassed = 0;

	public static int rightCarsSquished => rightCarsSpawned - rightCarsPassed;
	public static int leftCarsSquished => leftCarsSpawned - leftCarsPassed;

	public static void ResetValues()
	{
		rightCarsSpawned = 0;
		leftCarsSpawned = 0;
		rightCarsPassed = 0;
		leftCarsPassed = 0;
	}
}
