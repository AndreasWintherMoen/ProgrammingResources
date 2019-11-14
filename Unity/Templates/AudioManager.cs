using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager s_Instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
            }

            if (s_Instance == null)
            {
                GameObject obj = new GameObject("AudioManager");
                s_Instance = obj.AddComponent(typeof(AudioManager)) as AudioManager;
                Debug.Log("Could not locate a AudioManager object. AudioManager was generated automatically.");
            }
            return s_Instance;
        }
    }

    private float musicVolume = 0.5f;
    private float SFXVolume = 0.5f;
    private float masterVolume = 0.5f;

    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] Sound[] sounds;
    
    /// <summary>
    /// Plays sound at camera (middle of screen).
    /// </summary>
    /// <param name="name">String name of desired audio clip. Must match exact name in audioClipsList</param>
    /// <param name="volume">Volume of clip. Default = 1</param>
    /// <param name="loop">Should the clip replay after completion?</param>
    public void Play(string name, float pitch = 1f, bool loop = false)
    {
        AudioSource AS = FindAudioSource();
        AS.Stop();

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                AS.clip = sounds[i].clip;

                if (sounds[i].soundType == Sound.SoundType.MUSIC)
                {
                   volume = musicVolume * masterVolume;
                }
                else
                {
                   volume = SFXVolume * masterVolume;
                }

                break;
            }
        }

        if (AS.clip == null)
        {
            Debug.LogWarning("No audio clip set! Could not find clip: " + name);
            return;
        }

        volume = Mathf.Clamp01(volume);
        AS.pitch = pitch;
        AS.volume = volume;
        AS.loop = loop;
        AS.Play();
    }

    /// <summary>
    /// Finds an available audio source. If none are available, adds a new one. 
    /// </summary>
    private AudioSource FindAudioSource()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].isPlaying == false)
                return audioSources[i];
        }

        AudioSource AS = gameObject.AddComponent<AudioSource>();
        audioSources.Add(AS);
        Debug.Log("No available audio source found. Adding a new one. Total audio sources: " + audioSources.Count);

        return AS;
    }

    /// <summary>
    /// Stops playing a specific sound.
    /// </summary>
    /// <param name="name">Name of audio clip as given in the audio manager</param>
    public void StopPlaying(string name)
    {
        AudioSource[] ASList = GetComponents<AudioSource>();

        for (int i = 0; i < ASList.Length; i++)
        {
            if (ASList[i].isPlaying)
            {
                if (ASList[i].clip.name == name)
                {
                    ASList[i].Stop();
                }

            }
        }
    }

    /// <summary>
    /// Stops playing all sounds
    /// </summary>
    public void StopAllSounds()
    {
        AudioSource[] ASList = GetComponents<AudioSource>();

        for (int i = 0; i < ASList.Length; i++)
        {
            if (ASList[i].isPlaying)
            {
                ASList[i].Stop();
            }
        }
    }

    /// <summary>
    /// Stops playing all sounds of a given sound type
    /// </summary>
    /// <param name="type">The sound type to be stopped (either SFX or Music)</param>
    public void StopAllSounds(Sound.SoundType type)
    {
        AudioSource[] ASList = GetComponents<AudioSource>();

        for (int i = 0; i < ASList.Length; i++)
        {
            if (ASList[i].isPlaying && FindSound(ASList[i].clip.name).soundType == type)
            {
                ASList[i].Stop();
            }
        }
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return SFXVolume;
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;

        UpdateVolume();
    }

    public void SetSFXVolume(float value)
    {
        SFXVolume = value;

        UpdateSFXVolume();
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;

        UpdateVolume();
    }

    private Sound FindSound(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
                return sound;
        }
        Debug.LogWarning("Sound not found!");
        return null;
    }

    private void UpdateVolume()
    {
        foreach (AudioSource AS in audioSources)
        {
            if (AS.clip == null)
                return;

            Sound sound = FindSound(AS.clip.name);
            if (sound.soundType == Sound.SoundType.MUSIC)
                AS.volume = masterVolume * musicVolume;
            else if (sound.soundType == Sound.SoundType.SFX)
                AS.volume = masterVolume * SFXVolume;
        }
    }

    private void UpdateMusicVolume()
    {
        foreach (AudioSource AS in audioSources)
        {
            if (AS.clip == null)
                return;

            Sound sound = FindSound(AS.clip.name);
            if (sound.soundType == Sound.SoundType.MUSIC)
                AS.volume = masterVolume * musicVolume;
        }
    }

    private void UpdateSFXVolume()
    {
        foreach (AudioSource AS in audioSources)
        {
            if (AS.clip == null)
                return;

            Sound sound = FindSound(AS.clip.name);
            if (sound.soundType == Sound.SoundType.SFX)
                AS.volume = masterVolume * SFXVolume;
        }
    }
}
