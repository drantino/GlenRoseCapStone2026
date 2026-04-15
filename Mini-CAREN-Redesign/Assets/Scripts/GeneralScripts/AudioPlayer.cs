using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows any class to play sounds from a single audio source.
/// To use it: Place a single instance of the audioPlayer prefab in the scene, and place the sounds that you plan to use in the audioSOList
/// Please be careful when modifying this class as to not break other games.
/// </summary>
public class AudioPlayer : MonoBehaviour
{
	private static AudioPlayer instance;
	
	public static float masterVolume = 1;

	public float masterVol; // Temp debugging

	[SerializeField] private AudioSO[] audioSOList; // list of audioSO's that will be used in the current scene (for optimization, only include audio that will be played in this scene)
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

	private void Update()
	{
		masterVol = masterVolume;
	}

	private void OnDestroy()
	{
		instance = null;
	}

	/// <summary>
	/// Play a sound.
	/// Note that an audioPlayer must be in the scene, and it must contain the sound your trying to play
	/// </summary>
	/// <param name="sound">The sound to play</param>
	/// <param name="volume">the volume multiplier of the sound</param>
	/// <exception cref="System.Exception"></exception>
	public static void Play(Sound sound, float volume = 1)
	{
		if (instance == null)
		{
			Debug.LogWarning($"Cannot play sound '{sound.ToString()}' because there is no AudioPlayer in the scene.");
			return;
		}

		if (!instance.audioSODict.TryGetValue(sound, out AudioSO audioSO))
		{
			Debug.LogWarning($"Cannot play sound {sound.ToString()} because that sound has not been loaded into the AudioPlayer of this scene.");
			return;
		}

		if (audioSO.audioClip == null)
			throw new System.Exception($"audioSO '{audioSO.name}' does not have an audioClip");

		float totalVolume = audioSO.volume * volume * masterVolume;

		if (totalVolume > 3)
		{
			Debug.LogWarning("audioClip volume cannot exceed 3.");
			return;
		}

		instance.audioSource.PlayOneShot(audioSO.audioClip, totalVolume);
	}
}
