using System.Collections;
using UnityEngine;

public class AudioLoop : MonoBehaviour
{
    private float originalVolume;

    [HideInInspector]
    public AudioSource audioSource;

    private Coroutine fadeInCoroutine;
    private Coroutine fadeOutCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

		if (audioSource.resource == null)
			throw new System.Exception("Cannot fade in audio because the audio resource has not been set.");

        originalVolume = audioSource.volume;
	}

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void FadeIn(float seconds)
    {
        if (fadeOutCoroutine != null) StopCoroutine(fadeOutCoroutine);
        fadeInCoroutine = StartCoroutine(FadeInCoroutine(seconds));
    }

    public void FadeOut(float seconds)
    {
		if (fadeInCoroutine != null) StopCoroutine(fadeInCoroutine);
		fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(seconds));
	}

	private IEnumerator FadeInCoroutine(float seconds)
	{
		audioSource.Play();

		float timeStamp = 0;
		while (timeStamp <= seconds)
		{
			audioSource.volume = (timeStamp / seconds) * originalVolume;

			timeStamp += Time.deltaTime;
			yield return null;
		}

		audioSource.volume = (timeStamp / seconds) * originalVolume;

		fadeInCoroutine = null;
	}

	private IEnumerator FadeOutCoroutine(float seconds)
    {
        float timeStamp = seconds;
        while (timeStamp >= 0)
        {
            audioSource.volume = (timeStamp / seconds) * originalVolume;

            timeStamp -= Time.deltaTime;
            yield return null;
        }
        
		audioSource.Stop();
        fadeOutCoroutine = null;
    }
}
