using UnityEngine;
using UnityEngine.InputSystem;

public class FootController : MonoBehaviour
{
    public GameObject leftFootPrefab;
    public GameObject rightFootPrefab;

    public Vector3 leftFootPosition;
    public Vector3 rightFootPosition;

    public GameObject leftFoot;
    public GameObject rightFoot;

    public float minimumHeight;
    public float maximumHeight;
    public float heightThreshold;

    //DEBUG CONTROLS
    public float movementMultiplyer;
    float leftMovement, rightMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftFoot = Instantiate(leftFootPrefab);
        rightFoot = Instantiate(rightFootPrefab);

        //TODO:identify left and right foot markers

        leftFoot.transform.position = leftFootPosition;
        rightFoot.transform.position = rightFootPosition;

    }

    // Update is called once per frame
    void Update()
    {

        #region Debug Controls
        //DEBUG CONTROLS
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
        #endregion

    }
    private void FixedUpdate()
    {
        //TODO: update foot position to left and right markers

        #region Debug Controls

        //DEBUG CONTROLS
        if (leftFoot.transform.position.y < maximumHeight && leftMovement > 0
            || leftFoot.transform.position.y > minimumHeight && leftMovement < 0)
        {
            leftFootPosition.y += leftMovement * Time.deltaTime * movementMultiplyer;
        }
        if (rightFoot.transform.position.y < maximumHeight && rightMovement > 0
            || rightFoot.transform.position.y > minimumHeight && rightMovement < 0)
        {
            rightFootPosition.y += rightMovement * Time.deltaTime * movementMultiplyer;
        }
        #endregion

        leftFoot.transform.position = leftFootPosition;
        rightFoot.transform.position = rightFootPosition;

    }
}
