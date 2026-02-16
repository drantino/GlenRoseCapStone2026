using UnityEngine;

public class TrafficJamScoreKeeper : MonoBehaviour
{
	public enum Type
	{
		LeftFoot,
		RightFoot
	}
	
	public static int leftFootVehiclesPassed;
	public static int rightFootVehiclesPassed;
	public static int leftFootVehiclesSquished;
	public static int rightFootVehiclesSquished;

	[SerializeField] private Type type; // determines which foot (left or right) this score keeper is scoring. 
	[SerializeField] private GameObject scorePopupPrefab;
	[SerializeField] private string vehiclePassedText; // the text ui that will pop up when a vehicle passes.
	[SerializeField] private string vehicleSquishedText; // the text ui that will pop up when a vehicle is squished.

	private string vehicleFootTag; // the score keeper will only count points for vehicles with this foot tag.

	private void Start()
	{
		if (type == Type.LeftFoot)
			vehicleFootTag = "LeftShoe";
		else
			vehicleFootTag = "RightShoe";

		ResetValues();
	}

	public static void ResetValues()
	{
		leftFootVehiclesPassed = 0;
		leftFootVehiclesSquished = 0;
		rightFootVehiclesPassed = 0;
		rightFootVehiclesSquished = 0;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Vehicle")) return;

		Vehicle vehicle = other.transform.parent.GetComponent<Vehicle>(); // we access the component of the parent because the hitbox is a child of the vehicle object

		if (vehicle.footTag == vehicleFootTag)
		{
			if (!vehicle.squished)
			{
				// the car made it through
				if (type == Type.LeftFoot) leftFootVehiclesPassed++;
				else rightFootVehiclesPassed++;

				// provide player feedback
				ScorePopup scorePopup = Instantiate(scorePopupPrefab, vehicle.transform.position + new Vector3(0, 2, 0), Quaternion.identity).GetComponent<ScorePopup>();
				scorePopup.color = new Color(0, 1, 0);
				scorePopup.text = vehiclePassedText;
			}
			else
			{
				// the car was squished
				if (type == Type.LeftFoot) leftFootVehiclesSquished++;
				else rightFootVehiclesSquished++;

				// provide player feedback
				ScorePopup scorePopup = Instantiate(scorePopupPrefab, vehicle.transform.position + new Vector3(0, 2, 0), Quaternion.identity).GetComponent<ScorePopup>();
				scorePopup.color = new Color(1, 0, 0);
				scorePopup.text = vehicleSquishedText;
			}
		}
	}
}
