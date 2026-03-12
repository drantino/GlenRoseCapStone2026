using UnityEngine;

public enum Sound
{
	GameStart = 0,
	Countdown = 1,
	CarSquish = 2,
}

[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class AudioSO : ScriptableObject
{
	public Sound soundName;
	public AudioClip audioClip;
	public float volume = 1;
}
