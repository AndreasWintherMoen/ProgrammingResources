using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public enum SoundType { SFX, MUSIC };
    public SoundType soundType;
}
