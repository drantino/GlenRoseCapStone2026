using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private GameObject greenLightModel;
    [SerializeField] private GameObject redLightModel;

    public bool redLightOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (redLightModel == null)
            throw new System.Exception("Red light model of traffic light was not set.");
		if (greenLightModel == null)
			throw new System.Exception("Green light model of traffic light was not set.");

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
		greenLightModel.SetActive(!redLightOn);
		redLightModel.SetActive(redLightOn);
	}
}
