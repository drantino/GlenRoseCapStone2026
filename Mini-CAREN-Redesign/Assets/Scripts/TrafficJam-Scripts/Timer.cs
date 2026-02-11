using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
	private static Timer instance;

	[SerializeField] private TextMeshProUGUI timerTextMesh;
	[SerializeField] public float startTime;

	private float time;
	private bool paused = false;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		time = startTime;
		paused = false;

		if (timerTextMesh == null)
			throw new System.Exception($"timerTextMesh is null");
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private void Update()
	{
		if (paused) return;

		// update the time
		if (time >= 0)
			time -= Time.deltaTime;
		else
			time = 0;

		// update the UI
		timerTextMesh.text = timerTextMesh.text = $"<mspace=0.5em>{Mathf.CeilToInt(time).ToString()}</mspace>";
	}

	public static void ResetTimer()
	{
		if (instance == null)
			throw new System.Exception("Cannot reset the timer because there is no timer in the scene");

		instance.paused = true;
		instance.time = instance.startTime;
	}

	public static void Pause()
	{
		if (instance == null)
			throw new System.Exception("Cannot pause the timer because there is no timer in the scene");

		instance.paused = true;
	}

	public static void Play()
	{
		if (instance == null)
			throw new System.Exception("Cannot play the timer because there is no timer in the scene");

		instance.paused = false;
	}

	public static float GetTime()
	{
		if (instance == null)
			throw new System.Exception("Cannot get the time because there is no timer in the scene");

		return instance.time;
	}
}
