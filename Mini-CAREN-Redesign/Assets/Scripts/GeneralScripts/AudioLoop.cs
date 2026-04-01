using NUnit.Framework.Constraints;
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

        if (audioSource == null )
			throw new System.Exception($"the audio loop '{gameObject.name}' must have an audioSource component.");

		if (audioSource.resource == null)
			throw new System.Exception($"the audio resource of audio loop '{gameObject.name}' has not been set.");

        originalVolume = audioSource.volume;
	}

    public void Play()
    {
        if (audioSource.resource == null)
        {
            Debug.LogWarning($"the audio resource of audio loop '{gameObject.name}' has not been set.");
            return;
        }

        audioSource.Play();
    }

    public void Stop()
    {
		if (audioSource.resource == null)
		{
			Debug.LogWarning($"the audio resource of audio loop '{gameObject.name}' has not been set.");
			return;
		}

		audioSource.Stop();
    }

    public void FadeIn(float seconds)
    {
		if (audioSource.resource == null)
		{
			Debug.LogWarning($"the audio resource of audio loop '{gameObject.name}' has not been set.");
			return;
		}

		if (fadeOutCoroutine != null) StopCoroutine(fadeOutCoroutine);
        fadeInCoroutine = StartCoroutine(FadeInCoroutine(seconds));
    }

    public void FadeOut(float seconds)
    {
		if (audioSource.resource == null)
		{
			Debug.LogWarning($"the audio resource of audio loop '{gameObject.name}' has not been set.");
			return;
		}

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
