using UnityEngine;

public class IntersectionVehicleSpeedAdjuster : MonoBehaviour
{
    public float carSpeedUnderft_L, carSpeedUnderft_R;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vehicle")
        {
            Vehicle tmp = other.transform.parent.gameObject.GetComponent<Vehicle>();
            if(tmp.footTag == "LeftShoe")
            {
                tmp.moveSpeed *= carSpeedUnderft_L;
            }
            else
            {
                tmp.moveSpeed *= carSpeedUnderft_R;
            }
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vehicle")
        {
            Vehicle tmp = other.transform.parent.gameObject.GetComponent<Vehicle>();
            if(tmp.footTag == "LeftShoe")
            {
                tmp.moveSpeed /= carSpeedUnderft_L;
            }
            else
            {
                tmp.moveSpeed /= carSpeedUnderft_R;
            }
        } 
    }
}
