using UnityEngine;
using UnityEngine.InputSystem;

public class FootController : MonoBehaviour
{
    public GameObject leftFootPrefab;
    public GameObject rightFootPrefab;

    public Vector3 leftFootPosition;
    public Vector3 rightFootPosition;

    [HideInInspector] public GameObject leftFoot;
    [HideInInspector] public GameObject rightFoot;
    public BoxCollider leftFootCollider;
    public BoxCollider rightFootCollider;

    public float minimumHeight;
    public float maximumHeight;
    public float heightThreshold;

    //DEBUG CONTROLS
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
        //TODO:identify left and right foot markers

        
        

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
        #endregion
        //Set visual feet to read position of markers TODO: decouple X and Z, TODO: allow recalibrate to set the new "zero" point
        if(leftFoot != null)
        {
            leftFoot.transform.position = leftFootPosition;
        }
        if(rightFoot !=null)
        {
            rightFoot.transform.position = rightFootPosition;
        }
 
        //Height controller for colliders
        if(leftFootPosition.y > minimumHeight + heightThreshold)
        {
            leftFootCollider.enabled = false;
        }
        else
        { 
            leftFootCollider.enabled = true; 
        }
        if (rightFootPosition.y > minimumHeight + heightThreshold)
        {
            rightFootCollider.enabled = false;
        }
        else
        {
            rightFootCollider.enabled = true;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(leftFootPosition, new Vector3(0.1f,0.1f,0.1f));
        Gizmos.DrawCube(rightFootPosition, new Vector3(0.1f, 0.1f, 0.1f));
    }
}
