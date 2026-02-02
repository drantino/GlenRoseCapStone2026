using Unity.VisualScripting;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public string footTag; // this determines what foot the vehicle will stop infont of, and be stomped by

	[SerializeField] private GameObject vehicleModel;
	[SerializeField] private GameObject vehicleSquishedModel;
	[SerializeField] private float moveSpeed;
	[SerializeField]
	private bool objectInfront = false;
	[SerializeField]
	private bool squished = false;

	private void Start()
	{
		vehicleModel.SetActive(true);
		vehicleSquishedModel.SetActive(false);
	}

	private void Update()
	{
		if (!objectInfront && !squished)
		{
			// move
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}

		if (squished)
		{
			// TODO: squished movement
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		string tag = other.tag;
		if (tag == footTag || tag == "Vehicle")
			objectInfront = true;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Collision!");

		if (tag == footTag)
			Squish();
	}

	private void Squish()
	{
		squished = true;
		vehicleModel.SetActive(false);
		vehicleSquishedModel?.SetActive(true);
	}
}
