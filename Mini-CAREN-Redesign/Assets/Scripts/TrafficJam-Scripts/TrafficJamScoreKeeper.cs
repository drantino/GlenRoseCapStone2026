using UnityEngine;

public class TrafficJamScoreKeeper : MonoBehaviour
{
    public enum Type
    {
        LeftFoot,
        RightFoot
    }


    [SerializeField] private TrafficJamGameManager gameManager;
    [SerializeField] private VehicleSpawner vehicleSpawner;
    [SerializeField] private Type type; // determines which foot (left or right) this score keeper is scoring. 
    [SerializeField] private GameObject scorePopupPrefab;
    [SerializeField] private string vehiclePassedText; // the text ui that will pop up when a vehicle passes.
    [SerializeField] private string vehicleSquishedText; // the text ui that will pop up when a vehicle is squished.
    [SerializeField] private bool spawnPopups;
    [SerializeField] private ScorePopupPool scorePopupPool;

    private string vehicleFootTag; // the score keeper will only count points for vehicles with this foot tag.


    private void Start()
    {
        if (type == Type.LeftFoot)
            vehicleFootTag = "LeftShoe";
        else
            vehicleFootTag = "RightShoe";

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Vehicle")) return;

        Vehicle vehicle = other.transform.parent.GetComponent<Vehicle>(); // we access the component of the parent because the hitbox is a child of the vehicle object

        if (vehicle.footTag == vehicleFootTag)
        {
            //vehicleSpawner.RemovingVehicle(other.transform.parent.gameObject);
            vehicleSpawner.VehicleCrossedIntersection();

            if (vehicle.squished || vehicle.detouring)
            {

                if (type == Type.LeftFoot)
                {
                    gameManager.leftAmount++;
                    if (vehicle.squished)
                        gameManager.leftSquished++;
                    else
                        gameManager.leftDetoured++;
                }
                else
                {
                    gameManager.rightAmount++;
                    if (vehicle.squished)
                        gameManager.rightSquished++;
                    else
                        gameManager.rightDetoured++;
                }

                // provide player feedback
                if (spawnPopups)
                {
                    ScorePopup scorePopup = scorePopupPool.Get();
                    if (vehicle.transform.position.x < -14)
                    {
                        scorePopup.transform.position = new Vector3(-14, vehicle.transform.position.y, vehicle.transform.position.z) + new Vector3(0, 2, 0);

                    }
                    else if (vehicle.transform.position.x > 17)
                    {
                        scorePopup.transform.position = new Vector3(17, vehicle.transform.position.y, vehicle.transform.position.z) + new Vector3(0, 2, 0);
                    }
                    else
                    {
                        scorePopup.transform.position = vehicle.transform.position + new Vector3(0, 2, 0);
                    }

                    scorePopup.color = new Color(1, 0, 0);
                    scorePopup.text = vehicleSquishedText;
                    scorePopup.Enable();
                }
            }
            else
            {

                if (type == Type.LeftFoot)
                {
                    gameManager.leftAmount++;
                    gameManager.leftPassed++;
                }
                else
                {
                    gameManager.rightAmount++;
                    gameManager.rightPassed++;
                }

                // provide player feedback
                if (spawnPopups)
                {
                    ScorePopup scorePopup = scorePopupPool.Get();
                    scorePopup.transform.position = vehicle.transform.position + new Vector3(0, 2, 0);
                    scorePopup.color = new Color(0, 1, 0);
                    scorePopup.text = vehiclePassedText;
                    scorePopup.Enable();
                }
            }
        }
    }
}
