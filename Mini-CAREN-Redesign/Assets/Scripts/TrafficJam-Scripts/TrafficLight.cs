using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private GameObject greenLightModel;
    [SerializeField] private GameObject redLightModel;
    [SerializeField] private GameObject stopCollider;

    public bool redLightOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (stopCollider == null)
            throw new System.Exception("Traffic light stop collider is null");

        UpdateModelAndCollider();
	}

    [ContextMenu("Turn on red light")]
    public void TurnOnRedLight()
    {
        redLightOn = true;
        UpdateModelAndCollider();
    }

	[ContextMenu("Turn on green light")]
	public void TurnOnGreenLight()
    {
        redLightOn = false;
        UpdateModelAndCollider();
    }

    private void UpdateModelAndCollider()
    {
		if (greenLightModel != null) greenLightModel.SetActive(!redLightOn);
		if (redLightModel != null) redLightModel.SetActive(redLightOn);

        stopCollider.SetActive(redLightOn);
	}
}
