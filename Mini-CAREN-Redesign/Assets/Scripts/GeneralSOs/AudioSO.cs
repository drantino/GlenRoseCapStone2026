using UnityEngine;

public enum Sound
{

}

[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class AudioSO : ScriptableObject
{
	public Sound soundName;
	public AudioClip audioClip;
	public float volume = 1;
}
