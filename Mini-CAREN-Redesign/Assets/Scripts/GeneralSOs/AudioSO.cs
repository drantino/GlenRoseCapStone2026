using UnityEngine;

public enum Sound // don't change indexes of values to preserve scriptable object values
{
	GameStart = 0,
	Countdown = 1,
	CarSquish = 2,
	Three = 3,
	Two = 4,
	One = 5,
	RoundEnd = 6,
	PointGained = 7,
	PointLost = 8,
}

/// <summary>
/// This class is to be used with the AudioPlayer. Its purpose is to tie together each Sound enum value with a corresponding audio clip.
/// </summary>
[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class AudioSO : ScriptableObject
{
	public Sound soundName;
	public AudioClip audioClip;
	public float volume = 1;
}
