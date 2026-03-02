using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FootController : MonoBehaviour
{
    public GameObject leftFootPrefab;
    public GameObject rightFootPrefab;
	public RawImage shoeRenderImage;
	public TrafficJamGameManager gameManager;

    public Vector3 leftFootPosition;
    public Vector3 rightFootPosition;

    [HideInInspector] public GameObject leftFoot;
    [HideInInspector] public GameObject rightFoot;
    public BoxCollider leftFootCollider;
    public BoxCollider rightFootCollider;

    public float minimumHeight;
    public float maximumHeight;
    public float heightThreshold;

	private bool rightShoeAboveThreshhold = false;
	private bool leftShoeAboveThreshold = false;

    //DEBUG CONTROLS
    public bool debugMode;
    public float movementMultiplyer;
    float leftMovement, rightMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(leftFootPrefab != null)
        {
            leftFoot = Instantiate(leftFootPrefab);
            leftFoot.transform.position = leftFootPosition;
        }
        if (rightFootPrefab != null)
        {
            rightFoot = Instantiate(rightFootPrefab);
            rightFoot.transform.position = rightFootPosition;
        }  

    }

    // Update is called once per frame
    void Update()
    {

        #region Debug Controls
        //DEBUG CONTROLS
        if (debugMode)
        {
			if (Input.GetKey(KeyCode.W))
			{
				leftMovement = 1;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				leftMovement = -1;
			}
			else
			{
				leftMovement = 0;
			}
			if (Input.GetKey(KeyCode.I))
			{
				rightMovement = 1;
			}
			else if (Input.GetKey(KeyCode.K))
			{
				rightMovement = -1;
			}
			else
			{
				rightMovement = 0;
			}
		}
        #endregion

    }
    private void FixedUpdate()
    {
        //TODO: update foot position to left and right markers
		if (!debugMode)
		{
			if (!CalibrationLogic.NotEnoughtMarkers)
			{
				leftFootPosition = new Vector3(leftFootPosition.x, CalibrationLogic.leftFootPosition.y, leftFootPosition.z);
				rightFootPosition = new Vector3(rightFootPosition.x, CalibrationLogic.rightFootPosition.y, rightFootPosition.z);
			}
		}

        #region Debug Controls
        //DEBUG CONTROLS
        if (debugMode)
        {
			if (leftFootPosition.y < maximumHeight && leftMovement > 0
			|| leftFootPosition.y > minimumHeight && leftMovement < 0)
			{
				leftFootPosition.y += leftMovement * Time.fixedDeltaTime * movementMultiplyer;
			}
			if (rightFootPosition.y < maximumHeight && rightMovement > 0
				|| rightFootPosition.y > minimumHeight && rightMovement < 0)
			{
				rightFootPosition.y += rightMovement * Time.fixedDeltaTime * movementMultiplyer;
			}
		}
        #endregion

		//Set visual feet to read position of markers TODO: allow recalibrate to set the new "zero" point
		if (leftFoot != null)
		{
			leftFoot.transform.position = leftFootPosition;
		}
		if (rightFoot != null)
		{
			rightFoot.transform.position = rightFootPosition;
		}

		//TODO: change min/max height and threshold to settings of the Game object gamesettings
		if (gameManager != null)
		{
			//Height controller for colliders
			if (leftFootPosition.y > minimumHeight + gameManager.settings.HeightThreshold)//might have to change this to allow for offset
			{
				leftFootCollider.enabled = false;
				leftShoeAboveThreshold = true;
			}
			else
			{
				leftFootCollider.enabled = true;
				leftShoeAboveThreshold = false;
			}
			if (rightFootPosition.y > minimumHeight + gameManager.settings.HeightThreshold)//might have to change this to allow for offset
			{
				rightFootCollider.enabled = false;
				rightShoeAboveThreshhold = true;
			}
			else
			{
				rightFootCollider.enabled = true;
				rightShoeAboveThreshhold = false;
			}

			if (leftShoeAboveThreshold || rightShoeAboveThreshhold)
			{
				shoeRenderImage.color = new Color(1, 1, 1, 0.5f);
			}
			else
			{
				shoeRenderImage.color = Color.white;
			}
		}
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(leftFootPosition, new Vector3(0.1f,0.1f,0.1f));
        Gizmos.DrawCube(rightFootPosition, new Vector3(0.1f, 0.1f, 0.1f));
    }
}
