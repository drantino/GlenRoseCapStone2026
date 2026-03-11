using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	private static AudioPlayer instance;

	[SerializeField] private AudioSO[] audioSOList;
	private Dictionary<Sound, AudioSO> audioSODict = new Dictionary<Sound, AudioSO>();

	// components
	AudioSource audioSource;

	private void Awake()
	{
		instance = this;

		audioSource = GetComponent<AudioSource>();

		// setup dict
		foreach (AudioSO audioSO in audioSOList)
		{
			if (audioSODict.ContainsKey(audioSO.soundName))
				throw new System.Exception($"Audio Player was given two audioSO's with the same sound name: {audioSO.soundName.ToString()}");

			audioSODict.Add(audioSO.soundName, audioSO);
		}
	}

	private void OnDestroy()
	{
		instance = null;
	}

	public static void Play(Sound sound, float volume = 1)
	{
		if (instance == null)
			throw new System.Exception($"Cannot play sound '{sound.ToString()}' because there is no AudioPlayer in the scene.");

		if (!instance.audioSODict.TryGetValue(sound, out AudioSO audioSO))
		{
			Debug.LogWarning($"Cannot play sound {sound.ToString()} because that sound has not been loaded into the AudioPlayer of this scene.");
			return;
		}

		if (audioSO.audioClip == null)
			throw new System.Exception($"audioSO '{audioSO.name}' does not have an audioClip");

		float totalVolume = audioSO.volume * volume;

		if (totalVolume > 3)
			throw new System.Exception("audioClip volume cannot exceed 3.");

		instance.audioSource.PlayOneShot(audioSO.audioClip, totalVolume);
	}
}
