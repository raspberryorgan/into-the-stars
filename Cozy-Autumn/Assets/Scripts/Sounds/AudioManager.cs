using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public int sources;
    AudioSource[] audioSources;

    public static AudioManager instance;

    [HideInInspector]
    public float sfxMultiplier = 1;
    [HideInInspector]
    public float musicVolume = 1;

    public AudioSource music;
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        foreach (Sound s in sounds)
        {

        }
        audioSources = new AudioSource[sources];
        for (int i = 0; i < sources; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].mute = true;
        }

        musicVolume = GameManager.Instance.data.musicVolume;
        music.volume = musicVolume;
        sfxMultiplier = GameManager.Instance.data.sfxVolume;
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null)
        {
            AudioSource asource = FindAudioSource();
            if (asource == null) return;
            asource.clip = s.clip;
            asource.volume = s.Volume * sfxMultiplier;
            asource.pitch = s.Pitch;
            asource.loop = s.loop;
            asource.Play();
            //Debug.Log("Playing " + soundName);
        }
        else
        {
            //Debug.Log(soundName + " not found.");
        }
    }
    public void PlayStep()
    {
        List<Sound> list = sounds.Where(x => x.name == "step").ToList();
        Sound s = list[UnityEngine.Random.Range(0, list.Count)];
        AudioSource asource = FindAudioSource();
        if (asource == null) return;
        asource.clip = s.clip;
        asource.volume = s.Volume * sfxMultiplier;
        asource.pitch = s.Pitch;
        asource.loop = s.loop;
        asource.Play();
    }
    AudioSource FindAudioSource()
    {
        AudioSource asource = audioSources.Where(x => x.isPlaying == false).FirstOrDefault();
        if (asource == null) return null;
        asource.mute = false;
        //Debug.Log(asource);
        return asource;
    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp(volume, 0, 1);
        GameManager.Instance.data.musicVolume = musicVolume;

        music.volume = musicVolume;
        GameManager.Instance.saveManager.Save();
    }
    public void SetSfxVolume(float volume)
    {
        sfxMultiplier = Mathf.Clamp(volume, 0, 1);
        GameManager.Instance.data.sfxVolume = sfxMultiplier;

        GameManager.Instance.saveManager.Save();
    }
    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
            music.clip = menuMusic;
        else
        {
            music.clip = levelMusic;
        }

        music.Play();
    }
}
