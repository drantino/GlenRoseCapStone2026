using UnityEngine;

public class TrafficJamScoreKeeper : MonoBehaviour
{
	[SerializeField] private string vehicleFootTag; // the score keeper will only count points for vehicles with this foot tag.
	[SerializeField] private GameObject scorePopupPrefab;
	[SerializeField] private string vehiclePassedText;
	[SerializeField] private string vehicleSquishedText;
	
	[HideInInspector] public int vehiclesPassed, vehiclesSquished = 0;

	private void Start()
	{
		Debug.Log("Starting");

		if (vehicleFootTag != "LeftShoe" && vehicleFootTag != "RightShoe")
			Debug.LogWarning("Variable 'vehicleFootTag' was not given either 'LeftShoe' or 'RightShoe' as a value. Please correct before running the project again.");

		ResetValues();
	}

	public void ResetValues()
	{
		vehiclesPassed = 0;
		vehiclesSquished = 0;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Trigger Entered");
		if (!other.CompareTag("Vehicle")) return;
		Debug.Log("Vehicle Detected");

		Vehicle vehicle = other.transform.parent.GetComponent<Vehicle>(); // we access the component of the parent because the hitbox is a child of the vehicle object

		if (vehicle.footTag == vehicleFootTag)
		{
			if (!vehicle.squished)
			{
				// the car made it through
				vehiclesPassed++;

				// provide player feedback
				ScorePopup scorePopup = Instantiate(scorePopupPrefab, vehicle.transform.position + new Vector3(0, 2, 0), Quaternion.identity).GetComponent<ScorePopup>();
				scorePopup.color = new Color(0, 1, 0);
				scorePopup.text = vehiclePassedText;
			}
			else
			{
				// the car was squished
				vehiclesSquished++;

				// provide player feedback
				ScorePopup scorePopup = Instantiate(scorePopupPrefab, vehicle.transform.position + new Vector3(0, 2, 0), Quaternion.identity).GetComponent<ScorePopup>();
				scorePopup.color = new Color(1, 0, 0);
				scorePopup.text = vehicleSquishedText;
			}
		}
	}
}
