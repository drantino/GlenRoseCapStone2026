using UnityEngine;

public class SetActiveOnAwake : MonoBehaviour
{
	private void Awake()
	{
		gameObject.SetActive(true);
	}
}
